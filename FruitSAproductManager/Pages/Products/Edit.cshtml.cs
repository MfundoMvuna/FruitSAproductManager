using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FruitSAproductManager.DataAccess.Entities;
using FruitSAproductManager.Services;

namespace FruitSAproductManager.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public EditModel(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        [BindProperty]
        public Product Product { get; set; }
        public SelectList CategoryList { get; set; }

        [BindProperty]
        public IFormFile ImageFile { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Product = await _productService.GetProductByIdAsync(id);

            if (Product == null)
            {
                return NotFound();
            }

            CategoryList = new SelectList(await _productService.GetAllCategoriesAsync(), "CategoryId", "Name");

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

        public async Task<IActionResult> OnPostAsync()
        {
            if (ImageFile != null && ImageFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await ImageFile.CopyToAsync(memoryStream);
                    Product.ImageUrl = memoryStream.ToArray();
                }
            }

            Product.CreatedBy = User.Identity.Name;
            var selectedCategory = await _categoryService.GetCategoryByIdAsync(Product.CategoryId);
            
            if (selectedCategory != null)
            {
                Product.CategoryName = selectedCategory.Name;
            }
            else
            {
                ModelState.AddModelError(string.Empty, "The selected category does not exist.");
                return Page();
            }


            try
            {
                await _productService.UpdateProductAsync(Product);
            }
            catch (Exception ex)
            {
                return RedirectToPage("/Error"); 
            }

            return RedirectToPage("./Overview");
        }
    }
}
