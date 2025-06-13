using Microsoft.AspNetCore.Identity;
using Orders_Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace Orders_Shared.Entities
{
    public class User : IdentityUser
    {
        [Display(Name = "Documento")]
        [Required(ErrorMessage = "El campo {0} no puede estar vacio.")]
        [MaxLength(20, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string Document { get; set; } = null!;

        [Display(Name = "Nombres")]
        [Required(ErrorMessage = "El campo {0} no puede estar vacio.")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string FirstName { get; set; } = null!;

        [Display(Name = "Apellidos")]
        [Required(ErrorMessage = "El campo {0} no puede estar vacio.")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string LastName { get; set; } = null!;

        [Display(Name = "Dirección")]
        [Required(ErrorMessage = "El campo {0} no puede estar vacio.")]
        [MaxLength(200, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string Address { get; set; } = null!;

        [Display(Name = "Foto")]
        public string? Photo { get; set; }

        [Display(Name = "Tipo de usuario")]
        public UserType UserType { get; set; }

        public City? City { get; set; }

        [Display(Name = "Ciudad")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una ciudad.")]
        public int CityId { get; set; }

        [Display(Name = "Usuario")]
        public string FullName => $"{FirstName} {LastName}";

        public ICollection<TemporalOrder>? TemporalOrders { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}