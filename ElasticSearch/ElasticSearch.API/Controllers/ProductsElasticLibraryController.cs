using ElasticSearch.API.DTOs;
using ElasticSearch.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ElasticSearch.API.Controllers
{
    /// <summary>
    /// ELASTIC.CLIENTS.ELASTICSEARCH KÜTÜPHANESİ İLE YAPILAN İŞLEM
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsElasticLibraryController : BaseController
    {
        private readonly IProductService _productService; 
        public ProductsElasticLibraryController(IProductService productService)
        {
            _productService = productService;
        }
         
        [HttpPost("Save")]
        public async Task<IActionResult> Save(ProductCreateDto request)
        {
            return CreateActionResult(await _productService.SaveAsyncNew(request));    
        }

         
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return CreateActionResult(await _productService.GetAllAsyncNew());
        }

        [HttpGet("Id")] 
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            return CreateActionResult(await _productService.GetByIdAsyncNew(id));
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateAsync(ProductUpdateDto productUpdate)
        {
            return CreateActionResult(await _productService.UpdateAsyncNew(productUpdate));
        }

        [HttpPost("Upsert")]
        public async Task<IActionResult> UpsertAsync(ProductUpsertDto product)
        {
            return CreateActionResult(await _productService.UpsertAsyncNew(product));
        }

        /// <summary>
        /// Hata Yönetimi için bu metod ele alınmıştır.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            return CreateActionResult(await _productService.DeleteAsyncNew(id));
        }
    }
}
