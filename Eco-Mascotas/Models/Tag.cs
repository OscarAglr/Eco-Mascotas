using System.ComponentModel.DataAnnotations;

namespace Eco_Mascotas.Models
{
    // Clase de etiquetas
    public class Tag
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Tag Name")]
        public string TagName { get; set; }
    }
}
