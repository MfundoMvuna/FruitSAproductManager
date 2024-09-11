using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitSAproductManager.DataAccess.Entities
{
    public class Product
    {
        [Key]
        [Required]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "The Product Code is required.")]
        [StringLength(50, ErrorMessage = "The Product Code cannot exceed 50 characters.")]
        public string ProductCode { get; set; }

        [Required(ErrorMessage = "The Name is required.")]
        [StringLength(100, ErrorMessage = "The Name cannot exceed 100 characters.")]
        public string Name { get; set; }
        public string? Description { get; set; }

        [Required(ErrorMessage = "The Category name is required.")]
        [StringLength(100, ErrorMessage = "The Category name cannot exceed 100 characters.")]
        public string CategoryName { get; set; }

        [Required]
        [Range(0, 100000, ErrorMessage = "The Price must be between 0 and 100,000.")]
        public decimal Price { get; set; }
        public byte[]? ImageUrl { get; set; }


        public int CategoryId { get; set; }
        public Category? Category { get; set; }


        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
    }
}
