using ElasticSearch.API.DTOs;
using ElasticSearch.API.Models.ModelsForNestLibrary;
using ElasticSearch.API.Repositories;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Net;
using System.Threading.Tasks;

namespace ElasticSearch.API.Services
{
    public interface IProductService
    {
        // NEST KÜTÜPHANESİ - START
        Task<ResponseDto<ProductDto>> SaveAsync(ProductCreateDto request);
        Task<ResponseDto<List<ProductDto>>> GetAllAsync();
        Task<ResponseDto<ProductDto>> GetByIdAsync(string id);
        Task<ResponseDto<bool>> UpdateAsync(ProductUpdateDto updateProduct);
        Task<ResponseDto<bool>> UpsertAsync(ProductUpsertDto request);
        Task<ResponseDto<bool>> DeleteAsync(string id);
        // NEST KÜTÜPHANESİ - END

        // ELASTIC.CLIENTS.ELASTICSEARCH KÜTÜPHANESİ - START
        Task<ResponseDto<ProductDto>> SaveAsyncNew(ProductCreateDto request);
        Task<ResponseDto<List<ProductDto>>> GetAllAsyncNew();
        Task<ResponseDto<ProductDto>> GetByIdAsyncNew(string id);
        Task<ResponseDto<bool>> UpdateAsyncNew(ProductUpdateDto updateProduct);
        Task<ResponseDto<bool>> UpsertAsyncNew(ProductUpsertDto request);
        Task<ResponseDto<bool>> DeleteAsyncNew(string id);
        // ELASTIC.CLIENTS.ELASTICSEARCH KÜTÜPHANESİ - START
    }
}
