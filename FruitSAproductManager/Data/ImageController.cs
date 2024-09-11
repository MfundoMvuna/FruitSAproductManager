using FruitSAproductManager.DataAccess.Entities;
using FruitSAproductManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace FruitSAproductManager.Data
{
    [Route("Image")]
    public class ImageController : Controller
    {
        private readonly IProductService _productService; 

        public ImageController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("GetImage/{id}")]
        public async Task<IActionResult> GetImage(int id)
        {
            var imageData = await _productService.GetProductImageByIdAsync(id); 

            if (imageData == null || imageData.Length == 0)
            {
                return NotFound();
            }

            var base64Image = Convert.ToBase64String(imageData);

            return Json(new { base64Image });
        }
    }
}
