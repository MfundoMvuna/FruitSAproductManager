using FruitSAproductManager.DataAccess.Entities;
using FruitSAproductManager.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FruitSAproductManager.Pages.Products
{
    public class DetailsModel : PageModel
    {
        private readonly IProductService _productService;

        public DetailsModel(IProductService productService)
        {
            _productService = productService;
        }

        [BindProperty]
        public Product Product { get; set; }

        public async Task OnGetAsync(int id)
        {
            Product = await _productService.GetProductByIdAsync(id);

            if (Product == null)
            {
                 NotFound();
                return;
            }

            Page();
        }

        public async Task<IActionResult> OnGetProductImageAsync(int id)
        {
            var imageData = await _productService.GetProductImageByIdAsync(id);

            if (imageData == null || imageData.Length == 0)
            {
                return NotFound();
            }

            var base64Image = Convert.ToBase64String(imageData);
            return new JsonResult(new { base64Image });
        }

    }
}
