using Orders_Shared.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Orders_Shared.Entities
{
    public class Country : IEntityWithName
    {
        public int Id { get; set; }

        [Display(Name = "País")]
        [Required(ErrorMessage = "El campo {0} no puede estar vacio.")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string Name { get; set; } = null!;
        public ICollection<State>? States { get; set; }

        [Display(Name = "Departamentos / Estados")]
        public int StatesNuember => States == null || States.Count == 0 ? 0 : States.Count; 
    }
}