using Backend_Test.Models;
using Backend_Test.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace Backend_Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult> Listar()
        {
            try
            {
                var lista = await _categoryRepository.Lista();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener la lista de Categorys: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] Category category)
        {
            if (category == null)
            {
                return BadRequest("El objeto Category no puede ser null");
            }

            try
            {
                var resultado = await _categoryRepository.Crear(category);
                if (resultado)
                {
                    return Ok(new { isSuccess = resultado });
                }
                return BadRequest("Error al crear la category");
            } catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear la Category: {ex.Message}");
            }

        }

        [HttpPut("{categoryId}")]
        public async Task<IActionResult> Editar(int categoryId, [FromBody] Category category)
        {
            if (category == null || category.CategoryId != categoryId)
            {
                return BadRequest("El objeto Category no puede ser null y debe tener un ID válido.");
            }

            try
            {
                var resultado = await _categoryRepository.Editar(category);
                if (resultado)
                {
                    return Ok(new { isSuccess = resultado });
                }
                return NotFound($"Category con ID {categoryId} no encontrado o no se pudo editar");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al editar el category: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            try
            {
                var resultado = await _categoryRepository.Eliminar(id);
                if (resultado)
                {
                    return NoContent();
                }
                return NotFound($"Category con ID {id} no encontrado o no se pudo editar");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar la Category {id}: {ex.Message}");
            }

        }
    }
}
