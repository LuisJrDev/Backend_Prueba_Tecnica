namespace Backend_Test.Models
{
    public class PostDetallesDTO
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<Category> Categorias { get; set; } = new List<Category>();
        public List<Comment> Comentarios { get; set; } = new List<Comment>();
    }

}
