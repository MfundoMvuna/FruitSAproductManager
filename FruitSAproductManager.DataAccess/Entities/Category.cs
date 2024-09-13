using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitSAproductManager.DataAccess.Entities
{
    public class Category
    {
        [Key]
        [Required]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "The Category Name is required.")]
        [StringLength(100, ErrorMessage = "The Category Name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Category Code is required.")]
        [RegularExpression(@"^[A-Z]{3}[0-9]{3}$", ErrorMessage = "Category code must be in the format ABC123 (3 letters followed by 3 numbers).")]
        public string CategoryCode { get; set; }

        [Required(ErrorMessage = "The IsActive field is required.")]
        public bool IsActive { get; set; }

        [Timestamp]  // Concurrency token
        public byte[]? RowVersion { get; set; }


        public ICollection<Product>? Products { get; set; }
    }
}
