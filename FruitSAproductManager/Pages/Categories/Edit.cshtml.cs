using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FruitSAproductManager.DataAccess.Entities;
using FruitSAproductManager.Services;

namespace FruitSAproductManager.Pages.Categories
{
    public class EditModel : PageModel
    {
        private readonly ICategoryService _categoryService;

        public EditModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [BindProperty]
        public Category Category { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Category = await _categoryService.GetCategoryByIdAsync(id);

            if (Category == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnGetIsActiveByIDAsync(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return new JsonResult(new { IsActive = category.IsActive });
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var categorysActiveId = Category.CategoryId;

            var categoryToUpdate = await _categoryService.GetCategoryByIdAsync(categorysActiveId);

            if (categoryToUpdate == null)
            {
                return NotFound();
            }

            categoryToUpdate.Name = Category.Name;
            categoryToUpdate.CategoryCode = Category.CategoryCode;
            categoryToUpdate.IsActive = Request.Form["activeCategory"].Contains("on");

            // Update all products that belong to this category
            var productsToUpdate = await _categoryService.GetProductsByCategoryIdAsync(categorysActiveId);

            foreach (var product in productsToUpdate)
            {
                product.CategoryName = Category.Name; // Update the CategoryName field in the Product
            }

            // Update category and products in the database
            await _categoryService.UpdateCategoryAsync(categoryToUpdate);
            await _categoryService.UpdateProductsAsync(productsToUpdate);

            return RedirectToPage("./Index");
        }

    }
}
