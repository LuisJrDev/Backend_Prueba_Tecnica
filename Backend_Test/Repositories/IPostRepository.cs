using Backend_Test.Models;

namespace Backend_Test.Repositories
{
    public interface IPostRepository
    {
        Task<List<PostDTO>> Lista();
        Task<bool> Crear(PostDTO post);
        Task<bool> Editar(PostDTO post);
        Task<bool> Eliminar(int id);
        Task<PostDetallesDTO> Detalles(int id);
        Task<bool> AñadirCategorias(int postId, string categorias);

    }
}
