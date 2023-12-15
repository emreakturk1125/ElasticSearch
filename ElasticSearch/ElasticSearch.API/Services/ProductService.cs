using ElasticSearch.API.DTOs;
using ElasticSearch.API.Models.ModelsForNestLibrary;
using ElasticSearch.API.Repositories;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ElasticSearch.API.Services
{
    public class ProductService : IProductService   
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<ResponseDto<ProductDto>> SaveAsync(ProductCreateDto request)
        { 
            var response = await _productRepository.SaveAsync(request.CreateProduct());

            if (response == null)
            {
                return ResponseDto<ProductDto>.Fail(new List<string> { "Kayıt esnasında bir hata meydana geldi." }, HttpStatusCode.ServiceUnavailable);
            }

            return ResponseDto<ProductDto>.Success(response.CreatedDto(), HttpStatusCode.Created);
        }
    }
}
