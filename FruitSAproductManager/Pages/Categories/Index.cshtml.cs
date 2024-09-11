using Microsoft.AspNetCore.Mvc.RazorPages;
using FruitSAproductManager.DataAccess.Entities;
using FruitSAproductManager.DataAccess;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FruitSAproductManager.DataAccess.Context;
using FruitSAproductManager.Services;

namespace FruitSAproductManager.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly ICategoryService _categoryService;
        public IndexModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IList<Category> Categories { get; set; }

        public async Task OnGetAsync()
        {
            Categories = (IList<Category>)await _categoryService.GetCategoriesAsync();
        }
    }
}
