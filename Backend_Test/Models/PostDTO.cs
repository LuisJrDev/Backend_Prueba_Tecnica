using System;
using System.ComponentModel.DataAnnotations;

namespace Backend_Test.Models
{
    public class PostDTO
    {
        public int PostId { get; set; }

        [Required(ErrorMessage = "El título es obligatorio.")]
        [StringLength(200, ErrorMessage = "El título no puede exceder los 200 caracteres.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "El Contenido Es obligatorio.")]
        [MinLength(10, ErrorMessage = "El contenido debe tener al menos 10 caracteres.")]
        public string Content { get; set; }

        [Required(ErrorMessage = "La fecha de creación es obligatoria.")]
        [DataType(DataType.DateTime, ErrorMessage = "La fecha de creación debe ser una fecha válida.")]
        public DateTime CreatedAt { get; set; }

        [Required(ErrorMessage = "La fecha de actualizacion es obligatoria.")]
        [DataType(DataType.DateTime, ErrorMessage = "La fecha de creación debe ser una fecha válida.")]
        public DateTime UpdatedAt { get; set; }
    }
}
