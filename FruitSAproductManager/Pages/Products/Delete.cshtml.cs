using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FruitSAproductManager.DataAccess.Entities;
using FruitSAproductManager.Services;

namespace FruitSAproductManager.Pages.Products
{
    public class DeleteModel : PageModel
    {
        private readonly IProductService _productService;

        public DeleteModel(IProductService productService)
        {
            _productService = productService;
        }

        [BindProperty]
        public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Product = await _productService.GetProductByIdAsync(id);

            if (Product == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnGetGetProductImageAsync(int id)
        {
            var imageData = await _productService.GetProductImageByIdAsync(id);

            if (imageData == null || imageData.Length == 0)
            {
                return NotFound();
            }

            var base64Image = Convert.ToBase64String(imageData);
            return new JsonResult(new { base64Image });
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product != null)
            {
                await _productService.DeleteProductAsync(id);
            }

            return RedirectToPage("./Overview");
        }
    }
}
