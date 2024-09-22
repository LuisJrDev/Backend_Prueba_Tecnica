using Backend_Test.Models;
using System.Data;
using System.Data.SqlClient;

namespace Backend_Test.Repositories
{
    public class ComentarioRepository : IComentarioRepository
    {
        private readonly string _conexion;

        public ComentarioRepository(IConfiguration configuration)
        {
            _conexion = configuration.GetConnectionString("ConexionSQL")!;
        }

        public async Task<bool> Crear(Comentario comentario)
        {
            using (var con = new SqlConnection(_conexion))
            {
                using (var cmd = new SqlCommand("sp_crearComment", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PostId", comentario.PostId);
                    cmd.Parameters.AddWithValue("@Content", comentario.Content);
                    await con.OpenAsync();
                    return await cmd.ExecuteNonQueryAsync() > 0;
                }
            }
        }
        public async Task<bool> Editar(Comentario comentario)
        {
            using (var con = new SqlConnection(_conexion))
            {
                using (var cmd = new SqlCommand("sp_editarComment", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CommentId", comentario.CommentId);
                    cmd.Parameters.AddWithValue("@Content", comentario.Content);
                    await con.OpenAsync();
                    return await cmd.ExecuteNonQueryAsync() > 0;
                }
            }
        }
        public async Task<bool> Eliminar(int id)
        {
            using (var con = new SqlConnection(_conexion))
            {
                using (var cmd = new SqlCommand("sp_eliminarComment", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CommentId", id);
                    await con.OpenAsync();
                    return await cmd.ExecuteNonQueryAsync() > 0;
                }
            }
        }
    }
}