using Backend_Test.Models;

namespace Backend_Test.Repositories
{
    public interface ICategoriaRepository
    {
        Task<List<Categoria>> Lista();
        Task<bool> Crear(Categoria categoria);
        Task<bool> Editar(Categoria categoria);
        Task<bool> Eliminar(int id);
    }
}
