using Orders_Shared.Entities;
using System.ComponentModel.DataAnnotations;

namespace Orders_Shared.DTOs
{
    public class UserDTO : User
    {
        [DataType(DataType.Password)] 
        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "La contraseña debe tener entre {2} y {1} caracteres")]
        public string Password { get; set; } = null!;

        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña")]
        [Required(ErrorMessage = "La confirmación de la contraseña es obligatoria")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "La confirmación de la contraseña debe tener entre {2} y {1} caracteres")]
        public string ConfirmPassword { get; set; } = null!;
    }
}