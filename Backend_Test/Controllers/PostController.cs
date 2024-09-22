using Backend_Test.Models;
using Backend_Test.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;

        public PostController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            try
            {
                var lista = await _postRepository.Lista();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener la lista de posts: {ex.Message}");
            }

        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] PostDTO post)
        {
            if (post == null)
            {
                return BadRequest("El objeto post no puede ser null");
            }

            try
            {
                var resultado = await _postRepository.Crear(post);
                if (resultado)
                {
                    return CreatedAtAction(nameof(Detalles), new { id = post.PostId }, post);
                }
                return BadRequest("Error al crear el post");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear el post: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Editar([FromBody] PostDTO post)
        {
            if (post == null)
            {
                return BadRequest("El objeto post no puede ser null");
            }

            try
            {
                var resultado = await _postRepository.Editar(post);
                if (resultado)
                {
                    return Ok(new { isSuccess = resultado });
                }
                return NotFound($"Post con ID {post.PostId} no encontrado o no se pudo editar");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al editar el post: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                var resultado = await _postRepository.Eliminar(id);
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

        [HttpGet("{id}")]
        public async Task<IActionResult> Detalles(int id)
        {
            try
            {
                var post = await _postRepository.Detalles(id);
                if (post == null)
                {
                    return NotFound($"Post con ID {id} no encontrado");
                }
                return Ok(post);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener el post con ID {id}: {ex.Message}");
            }
        }

        [HttpPost("{id}/categorias")]
        public async Task<IActionResult> AñadirCategorias(int id, [FromBody] string categorias)
        {
            if (string.IsNullOrEmpty(categorias))
            {
                return BadRequest("Las categorías no pueden estar vacías");
            }

            var resultado = await _postRepository.AñadirCategorias(id, categorias);
            if (resultado)
            {
                return Ok("Categorías añadidas exitosamente");
            }
            return StatusCode(500, "Error al añadir categorías al post");
        }
    }
}
