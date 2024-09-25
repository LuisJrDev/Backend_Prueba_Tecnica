using System.ComponentModel.DataAnnotations;

namespace Backend_Test.Models
{
    public class Comment
    {
        public int CommentId { get; set; }

        [Required(ErrorMessage = "El ID del Post es obligatorio.")]
        public int PostId { get; set; }

        [Required(ErrorMessage = "El contenido es obligatorio.")]
        [StringLength(300, ErrorMessage = "El contenido no puede exceder los 300 caracteres.")]
        public string Content { get; set; }

        [Required(ErrorMessage = "La fecha de creación es obligatoria.")]
        [DataType(DataType.DateTime, ErrorMessage = "La fecha de creación debe ser una fecha válida.")]
        public DateTime CreatedAt { get; set; }
    }
}
