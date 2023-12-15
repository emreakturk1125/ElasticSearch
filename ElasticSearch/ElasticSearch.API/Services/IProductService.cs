using ElasticSearch.API.DTOs;
using ElasticSearch.API.Models.ModelsForNestLibrary;
using ElasticSearch.API.Repositories;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ElasticSearch.API.Services
{
    public interface IProductService
    { 
        Task<ResponseDto<ProductDto>> SaveAsync(ProductCreateDto request); 
    }
}
