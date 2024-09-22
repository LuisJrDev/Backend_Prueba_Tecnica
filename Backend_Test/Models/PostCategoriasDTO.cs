namespace Backend_Test.Models
{
    public class PostCategoriasDTO
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<Categoria> Categorias { get; set; }
    }
}
