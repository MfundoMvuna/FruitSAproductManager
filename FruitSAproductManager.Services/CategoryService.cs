using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FruitSAproductManager.DataAccess.Entities;
using FruitSAproductManager.DataAccess.Context;

namespace FruitSAproductManager.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(int categoryId)
        {
            return await _context.Categories.FindAsync(categoryId);
        }

        public async Task AddCategoryAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            try
            {
                _context.Entry(category).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CategoryExists(category.CategoryId))
                {
                    throw new KeyNotFoundException($"Category with ID {category.CategoryId} not found.");
                }
                else
                {
                    // Handle concurrency exception
                    throw new DbUpdateConcurrencyException("The record you attempted to edit was modified by another user after you got the original value.");
                }
            }

        }

        public async Task<List<Product>> GetProductsByCategoryIdAsync(int categoryId)
        {
            return await _context.Products.Where(p => p.CategoryId == categoryId).ToListAsync();
        }

        public async Task UpdateProductsAsync(List<Product> products)
        {
            _context.Products.UpdateRange(products);
            await _context.SaveChangesAsync();
        }

        private async Task<bool> CategoryExists(int categoryId)
        {
            return await _context.Categories.AnyAsync(e => e.CategoryId == categoryId);
        }
    }

}
