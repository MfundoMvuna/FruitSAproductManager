using System;
using System.Linq;
using System.Threading.Tasks;
using FruitSAproductManager.DataAccess.Entities;
using FruitSAproductManager.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OfficeOpenXml.Table;
using OfficeOpenXml;

namespace FruitSAproductManager.Pages.Products
{
    public class OverviewModel : PageModel
    {
        private readonly IProductService _productService;

        public OverviewModel(IProductService productService)
        {
            _productService = productService;
        }

        public PaginatedList<Product> Products { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        public async Task OnGetAsync(int? pageNumber)
        {
            var pageSize = 10;

            var productList = await _productService.GetAllProductsAsync();

            var count = productList.Count;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            CurrentPage = pageNumber ?? 1;

            var paginatedProducts = productList
                .Skip((CurrentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            Products = new PaginatedList<Product>(paginatedProducts, count, CurrentPage, pageSize);
        }

        public async Task<IActionResult> OnGetDownloadAsync()
        {
            var products = await _productService.GetAllProductsAsync();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

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
