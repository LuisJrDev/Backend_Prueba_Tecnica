using System.ComponentModel.DataAnnotations;

namespace Backend_Test.Models
{
    public class Categoria
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "El titulo es obligatorio.")]
        [StringLength(50, ErrorMessage = "El titulo no puede exceder los 50 caracteres.")]
        [MinLength(5, ErrorMessage = "El contenido debe tener al menos 5 caracteres.")]
        public string Name { get; set; }

    }
}
