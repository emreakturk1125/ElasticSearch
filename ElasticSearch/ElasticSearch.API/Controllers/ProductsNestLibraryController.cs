using ElasticSearch.API.DTOs;
using ElasticSearch.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ElasticSearch.API.Controllers
{
    /// <summary>
    /// NEST KÜTÜPHANESİ İLE YAPILAN İŞLEM
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsNestLibraryController : BaseController
    {
        private readonly IProductService _productService; 
        public ProductsNestLibraryController(IProductService productService)
        {
            _productService = productService;
        }
         
        [HttpPost("Save")]
        public async Task<IActionResult> Save(ProductCreateDto request)
        {
            return CreateActionResult(await _productService.SaveAsync(request));    
        }

         
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return CreateActionResult(await _productService.GetAllAsync());
        }

        [HttpGet("Id")] 
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            return CreateActionResult(await _productService.GetByIdAsync(id));
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateAsync(ProductUpdateDto productUpdate)
        {
            return CreateActionResult(await _productService.UpdateAsync(productUpdate));
        }

        [HttpPost("Upsert")]
        public async Task<IActionResult> UpsertAsync(ProductUpsertDto product)
        {
            return CreateActionResult(await _productService.UpsertAsync(product));
        }

        /// <summary>
        /// Hata Yönetimi için bu metod ele alınmıştır.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            return CreateActionResult(await _productService.DeleteAsync(id));
        }
    }
}
