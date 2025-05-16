using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders_Shared.DTOs
{
    public class ChangePasswordDTO
    {
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña actual")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "El campo {0} debe de tener entre {2} y {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string CurrentPassword { get; set; } = null!;

        [DataType(DataType.Password)]
        [Display(Name = "Contraseña actual")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "El campo {0} debe de tener entre {2} y {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string NewPassword { get; set; } = null!;

        [Compare("NewPassword", ErrorMessage = "Las contraseñas no coinciden.")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña actual")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "El campo {0} debe de tener entre {2} y {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Confirm { get; set; } = null!;
    }
}
