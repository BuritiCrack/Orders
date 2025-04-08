using Orders_Shared.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Orders_Shared.Entities
{
    public class City : IEntityWithName
    {
        public int Id { get; set; }

        [Display(Name = "Ciudad")]
        [Required(ErrorMessage = "El campo {0} no puede estar vacio.")]
        [MaxLength(30, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string Name { get; set; } = null!;

        public int StateId { get; set; }
        public State? State { get; set; }
    }
}