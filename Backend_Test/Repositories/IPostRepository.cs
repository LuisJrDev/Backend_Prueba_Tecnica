using Backend_Test.Models;
using System.Collections;

namespace Backend_Test.Repositories
{
    public interface IPostRepository
    {
        Task<(IEnumerable<PostDTO> posts, int totalPosts)> ListarPosts(int page, int pageSize);
        Task<bool> Crear(PostDTO post);
        Task<bool> Editar(PostDTO post);
        Task<bool> Eliminar(int id);
        Task<PostDetallesDTO> Detalles(int id);
        Task<bool> AñadirCategorias(int postId, string categorias);

    }
}
