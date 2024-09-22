using Backend_Test.Models;
using Backend_Test.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComentarioController : ControllerBase
    {
        private readonly IComentarioRepository _comentarioRepository;

        public ComentarioController(IComentarioRepository comentarioRepository)
        {
            _comentarioRepository = comentarioRepository;
        }


        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] Comentario comentario)
        {
            if (comentario == null)
            {
                return BadRequest("El objeto post no puede ser null");
            }

            try
            {
                var resultado = await _comentarioRepository.Crear(comentario);
                if (resultado)
                {
                    return Ok(new { isSuccess = resultado });
                }
                return BadRequest("Error al crear el post");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear el post: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Editar([FromBody] Comentario comentario)
        {
            if (comentario == null)
            {
                return BadRequest("El objeto post no puede ser null");
            }

            try
            {
                var resultado = await _comentarioRepository.Editar(comentario);
                if (resultado)
                {
                    return Ok(new { isSuccess = resultado });
                }
                return NotFound($"Comentario con ID {comentario.CommentId} no encontrado o no se pudo editar");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al editar el Comentario: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                var resultado = await _comentarioRepository.Eliminar(id);
                if (resultado)
                {
                    return NoContent(); // 204
                }
                return NotFound($"Post con ID {id} no encontrado o no se pudo eliminar");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar el post con ID {id}: {ex.Message}");
            }
        }
    }
}
