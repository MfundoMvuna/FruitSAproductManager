using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FruitSAproductManager.DataAccess.Entities;
using FruitSAproductManager.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace FruitSAproductManager.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<Category?> GetCategoryByIdAsync(int categoryId);
        Task AddCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
    }
}
