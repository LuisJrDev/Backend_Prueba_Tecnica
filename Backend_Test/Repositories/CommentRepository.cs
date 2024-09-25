using Backend_Test.Models;
using System.Data;
using System.Data.SqlClient;

namespace Backend_Test.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly string _conexion;

        public CommentRepository(IConfiguration configuration)
        {
            _conexion = configuration.GetConnectionString("ConexionSQL")!;
        }

        public async Task<Comment> Crear(Comment comment)
        {
            using (var con = new SqlConnection(_conexion))
            {
                using (var cmd = new SqlCommand("sp_crearComment", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PostId", comment.PostId);
                    cmd.Parameters.AddWithValue("@Content", comment.Content);

                    var outputIdParam = new SqlParameter("@CommentId", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputIdParam);

                    await con.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                    comment.CommentId = (int)outputIdParam.Value;
                    return comment;
                }
            }
        }

        public async Task<bool> Editar(Comment Comment)
        {
            using (var con = new SqlConnection(_conexion))
            {
                using (var cmd = new SqlCommand("sp_editarComment", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CommentId", Comment.CommentId);
                    cmd.Parameters.AddWithValue("@Content", Comment.Content);
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