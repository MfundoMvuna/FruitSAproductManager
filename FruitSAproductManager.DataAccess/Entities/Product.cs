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
        [StringLength(10, ErrorMessage = "The Product Code cannot exceed 10 characters.")]
        [RegularExpression(@"^\d{6}-\d{3}$", ErrorMessage = "Product Code must be in the format yyyyMM-###")]
        public string ProductCode { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be at least 3 characters long")]
        public string Name { get; set; }
        public string? Description { get; set; }

        [Required(ErrorMessage = "The Category name is required.")]
        [StringLength(100, ErrorMessage = "The Category name cannot exceed 100 characters.")]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive number")]
        public decimal Price { get; set; }
        public byte[]? ImageUrl { get; set; }


        public int CategoryId { get; set; }
        public Category? Category { get; set; }


        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }

        [Timestamp]  // Concurrency token
        public byte[]? RowVersion { get; set; }
    }
}
