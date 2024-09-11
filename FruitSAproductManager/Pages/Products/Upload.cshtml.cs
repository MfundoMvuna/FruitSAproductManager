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
                    await file.CopyToAsync(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets[0];
                        var rowCount = worksheet.Dimension.Rows;

                        for (int row = 2; row <= rowCount; row++) // Assuming row 1 is the header
                        {
                            var product = new Product
                            {
                                ProductCode = worksheet.Cells[row, 1].Text,
                                Name = worksheet.Cells[row, 2].Text,
                                CategoryName = worksheet.Cells[row, 3].Text,
                                Price = decimal.Parse(worksheet.Cells[row, 4].Text)
                            };

                            // Save the product to the database
                            await _productService.AddProductAsync(product);
                        }
                    }
                }
            }

            return RedirectToPage("./Overview");
        }
    }
}
