using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using FruitSAproductManager.DataAccess.Entities;
using FruitSAproductManager.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OfficeOpenXml;
using OfficeOpenXml.Table;

namespace FruitSAproductManager.Pages.Products
{
    public class DownloadModel : PageModel
    {
        private readonly IProductService _productService;

        public DownloadModel(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> OnGetDownloadAsync()
        {
            var products = await _productService.GetAllProductsAsync();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Products");
                worksheet.Cells["A1"].LoadFromCollection(products, true, TableStyles.Medium9);

                // Format the header row
                worksheet.Cells[1, 1, 1, 5].Style.Font.Bold = true;

                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                var fileName = "Products.xlsx";
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }
    }
}
