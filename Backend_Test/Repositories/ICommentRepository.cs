using Backend_Test.Models;

namespace Backend_Test.Repositories
{
    public interface ICommentRepository
    {
        Task<Comment> Crear(Comment comment);
        Task<bool> Editar(Comment comment);
        Task<bool> Eliminar(int id);
    }
}
