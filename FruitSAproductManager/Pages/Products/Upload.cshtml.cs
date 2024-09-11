using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using FruitSAproductManager.DataAccess.Entities;
using FruitSAproductManager.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OfficeOpenXml;

namespace FruitSAproductManager.Pages.Products
{
    public class UploadModel : PageModel
    {
        private readonly IProductService _productService;

        public UploadModel(IProductService productService)
        {
            _productService = productService;
        }

        [BindProperty]
        public IFormFile UploadedFile { get; set; }

        public async Task<IActionResult> OnPostUploadAsync(IFormFile file)
        {

            if (file != null && file.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                    await file.CopyToAsync(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets[0];
                        var rowCount = worksheet.Dimension.Rows;

                        for (int row = 2; row <= rowCount; row++)
                        {
                            var productCode = worksheet.Cells[row, 2].Text; 
                            var name = worksheet.Cells[row, 3].Text;  
                            var description = worksheet.Cells[row, 4].Text;
                            var categoryName = worksheet.Cells[row, 5].Text;
                            var createdBy = worksheet.Cells[row, 11].Text;
                            var categoryId = worksheet.Cells[row, 8].Text;

                            if (decimal.TryParse(worksheet.Cells[row, 6].Text, out decimal price) && int.TryParse(categoryId, out int parsedCategoryId))
                            {
                                var product = new Product
                                {
                                    ProductCode = productCode,
                                    Name = name,
                                    Description = description,
                                    CategoryName = categoryName,
                                    CreatedDate = DateTime.Now,
                                    Price = price,
                                    CategoryId = parsedCategoryId,
                                    CreatedBy = createdBy
                                };

                                await _productService.AddProductAsync(product);
                            }
                            else
                            {
                                ModelState.AddModelError("", $"Invalid price format at row {row}.");
                                return Page();
                            }
                        }
                    }
                }
            }

            return RedirectToPage("./Overview");
        }
    }
}
