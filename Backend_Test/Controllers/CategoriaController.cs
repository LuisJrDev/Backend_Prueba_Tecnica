using Backend_Test.Models;
using Backend_Test.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaController(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            try
            {
                var lista = await _categoriaRepository.Lista();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener la lista de Categorias: {ex.Message}");
            }
        }

        [HttpPost]

        public async Task<IActionResult> Crear([FromBody] Categoria categoria)
        {
            if (categoria == null)
            {
                return BadRequest("El objeto Categoria no puede ser null");
            }

            try
            {
                var resultado = await _categoriaRepository.Crear(categoria);
                if (resultado)
                {
                    return Ok(new { isSuccess = resultado });
                }
                return BadRequest("Error al crear la categoria");
            } catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear la Categoria: {ex.Message}");
            }

        }


        [HttpPut]
        public async Task<IActionResult> Editar([FromBody] Categoria categoria)
        {
            if (categoria == null)
            {
                return BadRequest("El objeto Categoria no puede ser null");
            }

            try
            {
                var resultado = await _categoriaRepository.Editar(categoria);
                if (resultado)
                {
                    return Ok(new { isSuccess = resultado });
                }
                return NotFound($"Categoria con ID {categoria.CategoryId} no encontrado o no se pudo editar");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al editar la Categoria: {ex.Message}");
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                var resultado = await _categoriaRepository.Eliminar(id);
                if (resultado)
                {
                    return NoContent();
                }
                return NotFound($"Categoria con ID {id} no encontrado o no se pudo editar");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar la Categoria {id}: {ex.Message}");
            }

        }
    }
}
