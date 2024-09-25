using Backend_Test.Models;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Backend_Test.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly string _conexion;

        public PostRepository(IConfiguration configuration)
        {
            _conexion = configuration.GetConnectionString("ConexionSQL")!;
        }
        public async Task<(IEnumerable<PostDTO> posts, int totalPosts)> ListarPosts(int page, int pageSize)
        {
            using (var con = new SqlConnection(_conexion))
            {
                using (var cmd = new SqlCommand("sp_listaPosts", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Page", page);
                    cmd.Parameters.AddWithValue("@PageSize", pageSize);

                    var posts = new List<PostDTO>();
                    int totalPosts = 0;

                    await con.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            posts.Add(new PostDTO
                            {
                                PostId = (int)reader["PostId"],
                                Title = reader["Title"].ToString(),
                                Content = reader["Content"].ToString(),
                                CreatedAt = (DateTime)reader["CreatedAt"]
                            });
                        }

                        if (await reader.NextResultAsync() && await reader.ReadAsync()) 
                        {
                            totalPosts = (int)reader["totalPosts"];
                        }
                    }
                    return (posts, totalPosts);
                }
            }
        }
        public async Task<bool> Crear(PostDTO post)
        {
            using (var con = new SqlConnection(_conexion))
            {
                using (var cmd = new SqlCommand("sp_crearPosts", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Title", post.Title);
                    cmd.Parameters.AddWithValue("@Content", post.Content);
                    await con.OpenAsync();
                    return await cmd.ExecuteNonQueryAsync() > 0;
                }
            }
        }
        public async Task<bool> Editar(PostDTO post)
        {
            using (var con = new SqlConnection(_conexion))
            {
                using (var cmd = new SqlCommand("sp_editarPosts", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PostId", post.PostId);
                    cmd.Parameters.AddWithValue("@Title", post.Title);
                    cmd.Parameters.AddWithValue("@Content", post.Content);
                    await con.OpenAsync();
                    return await cmd.ExecuteNonQueryAsync() > 0;
                }
            }
        }
        public async Task<bool> Eliminar(int id)
        {
            using (var con = new SqlConnection(_conexion))
            {
                using (var cmd = new SqlCommand("sp_eliminarPosts", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PostId", id);
                    await con.OpenAsync();
                    return await cmd.ExecuteNonQueryAsync() > 0;
                }
            }
        }
        public async Task<PostDetallesDTO> Detalles(int id)
        {
            PostDetallesDTO post = null;
            using (var con = new SqlConnection(_conexion))
            {
                await con.OpenAsync();
                using (var cmd = new SqlCommand("sp_detallesPost", con))
                {
                    cmd.Parameters.AddWithValue("@PostId", id);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            post = new PostDetallesDTO
                            {
                                PostId = Convert.ToInt32(reader["PostId"]),
                                Title = reader["Title"].ToString(),
                                Content = reader["Content"].ToString(),
                                CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                                UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"]),
                                Categorias = new List<Category>(),
                                Comentarios = new List<Comment>()
                            };
                        }

                        if (await reader.NextResultAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                post.Categorias.Add(new Category
                                {
                                    CategoryId = Convert.ToInt32(reader["CategoryId"]),
                                    Name = reader["Name"].ToString()
                                });
                            }
                        }

                        if (await reader.NextResultAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                post.Comentarios.Add(new Comment
                                {
                                    CommentId = Convert.ToInt32(reader["CommentId"]),
                                    PostId = Convert.ToInt32(reader["PostId"]),
                                    Content = reader["Content"].ToString(),
                                    CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
                                });
                            }
                        }
                    }
                }
            }
            return post;
        }
        public async Task<bool> AñadirCategorias(int postId, string categorias)
        {
            using (var con = new SqlConnection(_conexion))
            {
                try
                {
                    await con.OpenAsync();
                    using (var cmd = new SqlCommand("sp_añadirCategoriesPost", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PostId", postId);
                        cmd.Parameters.AddWithValue("@Categorias", categorias);
                        return await cmd.ExecuteNonQueryAsync() > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    return false;
                }
            }

        }
    }
}