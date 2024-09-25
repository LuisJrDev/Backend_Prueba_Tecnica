using Backend_Test.Models;

namespace Backend_Test.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> Lista();
        Task<bool> Crear(Category category);
        Task<bool> Editar(Category category);
        Task<bool> Eliminar(int id);
    }
}
