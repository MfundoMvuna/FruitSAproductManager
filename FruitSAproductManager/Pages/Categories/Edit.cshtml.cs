using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FruitSAproductManager.DataAccess.Entities;
using FruitSAproductManager.Services;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;

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
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var categorysActiveId = Category.CategoryId;

            var categoryToUpdate = await _categoryService.GetCategoryByIdAsync(categorysActiveId);

            if (categoryToUpdate == null)
            {
                return NotFound();
            }

            // Update category fields
            categoryToUpdate.Name = Category.Name;
            categoryToUpdate.CategoryCode = Category.CategoryCode;
            categoryToUpdate.IsActive = Request.Form["activeCategory"].Contains("on");

            // Update all products that belong to this category
            var productsToUpdate = await _categoryService.GetProductsByCategoryIdAsync(categorysActiveId);

            foreach (var product in productsToUpdate)
            {
                product.CategoryName = Category.Name; // Update the CategoryName field in the Product
            }

            try
            {
                // Update category and products in the database
                await _categoryService.UpdateCategoryAsync(categoryToUpdate);
                await _categoryService.UpdateProductsAsync(productsToUpdate);
            }
            catch (DbUpdateConcurrencyException)
            {
                // Reload the entity from the database
                var databaseCategory = await _categoryService.GetCategoryByIdAsync(categorysActiveId);

                if (databaseCategory == null)
                {
                    return NotFound();
                }

                // Add an error message to display to the user
                ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                    + "was modified by another user after you. The changes were not saved.");

                // Optionally, you could merge database values with user input here if you want
                // to allow users to decide which values to keep.

                Category = databaseCategory; // Reload the category with current database values
                return Page(); // Redisplay the form
            }

            return RedirectToPage("./Index");
        }
    }
}
