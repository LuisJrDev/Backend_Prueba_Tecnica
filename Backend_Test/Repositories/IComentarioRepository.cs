using Backend_Test.Models;

namespace Backend_Test.Repositories
{
    public interface IComentarioRepository
    {
        Task<bool> Crear(Comentario comentario);
        Task<bool> Editar(Comentario comentario);
        Task<bool> Eliminar(int id);
    }
}
