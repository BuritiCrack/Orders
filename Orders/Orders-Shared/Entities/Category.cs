﻿using Orders_Shared.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Orders_Shared.Entities
{
    public class Category : IEntityWithName
    {
        public int Id { get; set; }

        [Display(Name = "Categoría")]
        [Required(ErrorMessage = "El campo {0} no puede estar vacio.")]
        [MaxLength(30, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string Name { get; set; } = null!;

        public ICollection<ProductCategory>? ProductCategories { get; set; }

        [Display(Name = "Productos")]
        public int ProductCategoriesNumber => ProductCategories == null || ProductCategories.Count == 0 ? 0 : ProductCategories.Count;

    }
}