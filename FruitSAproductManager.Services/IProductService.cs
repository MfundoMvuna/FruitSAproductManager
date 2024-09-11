using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FruitSAproductManager.DataAccess.Entities;

namespace FruitSAproductManager.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
        Task<byte[]> GetProductImageByIdAsync(int id);
        Task<List<Category>> GetAllCategoriesAsync();
        Task<string> GenerateProductCodeAsync();  
        Task<string> GetCategoryNameByIdAsync(int categoryId);
        Task AddProductsAsync(List<Product> products);
    }
}
