using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FruitSAproductManager.DataAccess.Entities;
using FruitSAproductManager.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace FruitSAproductManager.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<byte[]> GetProductImageByIdAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            return product?.ImageUrl;
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<string> GenerateProductCodeAsync()
        {
            var currentYearMonth = DateTime.Now.ToString("yyyyMM");
            var lastProduct = await _context.Products
                                .OrderByDescending(p => p.ProductId)
                                .FirstOrDefaultAsync();

            int nextSequence = 1;
            if (lastProduct != null)
            {
                var lastCode = lastProduct.ProductCode.Substring(7);
                int.TryParse(lastCode, out nextSequence);
                nextSequence++;
            }

            return $"{currentYearMonth}-{nextSequence:D3}";
        }

        public async Task<string> GetCategoryNameByIdAsync(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            return category?.Name;
        }

        public async Task AddProductsAsync(List<Product> products)
        {
            _context.Products.AddRange(products);
            await _context.SaveChangesAsync();
        }

    }
}
