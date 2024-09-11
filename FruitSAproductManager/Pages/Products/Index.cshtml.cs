using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FruitSAproductManager.DataAccess.Context;
using FruitSAproductManager.DataAccess.Entities;

namespace FruitSAproductManager.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Product> Products { get; set; }

        public async Task OnGetAsync()
        {
            Products = await _context.Products
                .Include(p => p.Category)
                .ToListAsync();
        }
    }
}
