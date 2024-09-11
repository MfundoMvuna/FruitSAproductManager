using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FruitSAproductManager.DataAccess.Entities;
using FruitSAproductManager.DataAccess;
using System.Threading.Tasks;
using FruitSAproductManager.DataAccess.Context;
using FruitSAproductManager.Services;

namespace FruitSAproductManager.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly ICategoryService _categoryService;

        public CreateModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [BindProperty]
        public Category Category { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            Category.IsActive = Request.Form["activeCategory"].Contains("on");

            await _categoryService.AddCategoryAsync(Category);

            return RedirectToPage("./Index");
        }
    }
}
