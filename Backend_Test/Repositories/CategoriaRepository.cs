using Backend_Test.Models;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace Backend_Test.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly string _conexion;

        public CategoriaRepository(IConfiguration configuration)
        {
            _conexion = configuration.GetConnectionString("ConexionSQL")!;
        }

        public async Task<List<Categoria>> Lista()
        {
            var lista = new List<Categoria>();
            using (var con = new SqlConnection(_conexion))
            {
                await con.OpenAsync();
                using (var cmd = new SqlCommand("sp_listarCategories", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            lista.Add(new Categoria
                            {
                                CategoryId = Convert.ToInt32(reader["CategoryId"]),
                                Name = reader["Name"].ToString()
                            });
                        }
                    }
                }
            }
            return lista;
        }
        public async Task<bool> Crear(Categoria categoria)
        {
            using (var con = new SqlConnection(_conexion))
            {
                using (var cmd = new SqlCommand("sp_crearCategory", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Name", categoria.Name);
                    await con.OpenAsync();
                    return await cmd.ExecuteNonQueryAsync() > 0;
                }
            }
        }
        public async Task<bool> Editar(Categoria categoria)
        {
            using (var con = new SqlConnection(_conexion))
            {
                using (var cmd = new SqlCommand("sp_editarCategory", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CategoryId", categoria.CategoryId);
                    cmd.Parameters.AddWithValue("@Name", categoria.Name);
                    await con.OpenAsync();
                    return await cmd.ExecuteNonQueryAsync() > 0;
                }
            }
        }
        public async Task<bool> Eliminar(int id)
        {
            using (var con = new SqlConnection(_conexion))
            {
                using (var cmd = new SqlCommand("sp_eliminarCategory", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CategoryId", id);
                    await con.OpenAsync();
                    return await cmd.ExecuteNonQueryAsync() > 0;
                }
            }
        }
    }

}