using System.Xml.Linq;

namespace Backend_Test.Models
{
    public class Post
    {
        public int PostId{ get; set; }
        public string Title{ get; set; }
        public string Content{ get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<Categoria> Categorias { get; set; } = new List<Categoria>();
        public List<Comentario> Comentarios { get; set; } = new List<Comentario>();
    }
}
