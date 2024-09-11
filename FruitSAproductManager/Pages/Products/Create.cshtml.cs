using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FruitSAproductManager.DataAccess.Entities;
using FruitSAproductManager.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FruitSAproductManager.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly IProductService _productService;

        public CreateModel(IProductService productService)
        {
            _productService = productService;
        }

        [BindProperty]
        public Product Product { get; set; }

        [BindProperty]
        public IFormFile ImageFile { get; set; }
        public SelectList CategoryList { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            CategoryList = new SelectList(await _productService.GetAllCategoriesAsync(), "CategoryId", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Product.CreatedDate = DateTime.Now;
            Product.CreatedBy = User.Identity.Name;

            Product.ProductCode = await _productService.GenerateProductCodeAsync();
            Product.CategoryName = await _productService.GetCategoryNameByIdAsync(Product.CategoryId);

            if (ImageFile != null && ImageFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await ImageFile.CopyToAsync(memoryStream);
                    Product.ImageUrl = memoryStream.ToArray();
                }
            }

            await _productService.AddProductAsync(Product);
            return RedirectToPage("./Overview");
        }
    }
}
