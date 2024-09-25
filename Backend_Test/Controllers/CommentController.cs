using Backend_Test.Models;
using Backend_Test.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;

        public CommentController(ICommentRepository CommentRepository)
        {
            _commentRepository = CommentRepository;
        }


        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] Comment comment)
        {
            if (comment == null)
            {
                return BadRequest("El objeto Comment no puede ser null");
            }

            try
            {
                var resultado = await _commentRepository.Crear(comment);
                if (resultado != null)
                {
                    return CreatedAtAction(nameof(Crear), new { id = resultado.CommentId }, resultado);
                }
                return BadRequest("Error al crear el Comment");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear el Comment: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Editar([FromBody] Comment Comment)
        {
            if (Comment == null)
            {
                return BadRequest("El objeto Comment no puede ser null");
            }

            try
            {
                var resultado = await _commentRepository.Editar(Comment);
                if (resultado)
                {
                    return Ok(new { isSuccess = resultado });
                }
                return NotFound($"Comment con ID {Comment.CommentId} no encontrado o no se pudo editar");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al editar el Comment: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                var resultado = await _commentRepository.Eliminar(id);
                if (resultado)
                {
                    return NoContent();
                }
                return NotFound($"Comment con ID {id} no encontrado o no se pudo eliminar");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar el Comment con ID {id}: {ex.Message}");
            }
        }
    }
}
