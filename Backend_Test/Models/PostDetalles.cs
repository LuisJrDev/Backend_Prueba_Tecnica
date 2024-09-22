namespace Backend_Test.Models
{
    public class PostDetalles
    {
        public Post Post { get; set; } = new Post();
        public List<Categoria> Categorias { get; set; } = new List<Categoria>();
        public List<Comentario> Comentarios { get; set; } = new List<Comentario>();
    }

}
