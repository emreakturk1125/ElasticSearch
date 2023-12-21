using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Requests;
using ElasticSearch.API.DTOs;
using ElasticSearch.API.Models.ModelsForNestLibrary;
using ElasticSearch.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nest;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ElasticSearch.API.Services
{
    public class ProductService : IProductService   
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductService> _logger;
        public ProductService(IProductRepository productRepository, ILogger<ProductService> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        #region NEST KÜTÜPHANESİ
        public async Task<ResponseDto<List<ProductDto>>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();

            var productListDto = products.Select(x => new ProductDto(
                                                      x.Id,
                                                      x.Name,
                                                      x.Price,
                                                      x.Stock,
                                                      new ProductFeatureDto(x.Feature.Width, x.Feature.Height, x.Feature.Color.ToString()))).ToList();
            return ResponseDto<List<ProductDto>>.Success(productListDto, HttpStatusCode.OK);
        }

        public async Task<ResponseDto<ProductDto>> SaveAsync(ProductCreateDto request)
        {
            var response = await _productRepository.SaveAsync(request.CreateProduct());

            if (response == null)
            {
                return ResponseDto<ProductDto>.Fail(new List<string> { "işlem esnasında bir hata meydana geldi." }, HttpStatusCode.ServiceUnavailable);
            }

            return ResponseDto<ProductDto>.Success(response.CreatedDto(), HttpStatusCode.Created);
        }

        public async Task<ResponseDto<ProductDto>> GetByIdAsync(string id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
            {
                return ResponseDto<ProductDto>.Fail("Ürün bulunamadı.", HttpStatusCode.NotFound);
            }

            var productDto = product.CreatedDto();

            return ResponseDto<ProductDto>.Success(productDto, HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<ResponseDto<bool>> UpdateAsync(ProductUpdateDto updateProduct)
        {
            var isSuccess = await _productRepository.UpdateAsync(updateProduct);
            if (!isSuccess)
            {
                return ResponseDto<bool>.Fail(new List<string> { "işlem esnasında bir hata meydana geldi." }, HttpStatusCode.InternalServerError);
            }
            return ResponseDto<bool>.Success(true, HttpStatusCode.NoContent);
        }

        [HttpPost]
        public async Task<ResponseDto<bool>> UpsertAsync(ProductUpsertDto request)
        {
            var isSuccess = await _productRepository.UpsertAsync(request);
            if (!isSuccess)
            {
                return ResponseDto<bool>.Fail(new List<string> { "işlem esnasında bir hata meydana geldi." }, HttpStatusCode.InternalServerError);
            }
            return ResponseDto<bool>.Success(true, HttpStatusCode.NoContent);
        }

        public async Task<ResponseDto<bool>> DeleteAsync(string id)
        {
            var deleteResponse = await _productRepository.DeleteAsync(id);

            if (!deleteResponse.IsValid && deleteResponse.Result == Nest.Result.NotFound)
            {
                return ResponseDto<bool>.Fail(new List<string> { "Silmeye çalıştığınız ürün bulunamamıştır." }, HttpStatusCode.NotFound);
            }


            if (!deleteResponse.IsValid)
            {
                _logger.LogError(deleteResponse.OriginalException, deleteResponse.ServerError.Error.ToString());
                return ResponseDto<bool>.Fail(new List<string> { "işlem esnasında bir hata meydana geldi." }, HttpStatusCode.InternalServerError);
            }
            return ResponseDto<bool>.Success(true, HttpStatusCode.NoContent);
        }
        #endregion

        #region ELASTIC.CLIENTS.ELASTICSEARCH KÜTÜPHANESİ

        public async Task<ResponseDto<List<ProductDto>>> GetAllAsyncNew()
        {
            var products = await _productRepository.GetAllAsyncNew();

            var productListDto = products.Select(x => new ProductDto(
                                                      x.Id,
                                                      x.Name,
                                                      x.Price,
                                                      x.Stock,
                                                      new ProductFeatureDto(x.Feature.Width, x.Feature.Height, x.Feature.Color.ToString()))).ToList();
            return ResponseDto<List<ProductDto>>.Success(productListDto, HttpStatusCode.OK);
        }

        public async Task<ResponseDto<ProductDto>> SaveAsyncNew(ProductCreateDto request)
        {
            var response = await _productRepository.SaveAsyncNew(request.CreateProduct());

            if (response == null)
            {
                return ResponseDto<ProductDto>.Fail(new List<string> { "işlem esnasında bir hata meydana geldi." }, HttpStatusCode.ServiceUnavailable);
            }

            return ResponseDto<ProductDto>.Success(response.CreatedDto(), HttpStatusCode.Created);
        }

        public async Task<ResponseDto<ProductDto>> GetByIdAsyncNew(string id)
        {
            var product = await _productRepository.GetByIdAsyncNew(id);

            if (product == null)
            {
                return ResponseDto<ProductDto>.Fail("Ürün bulunamadı.", HttpStatusCode.NotFound);
            }

            var productDto = product.CreatedDto();

            return ResponseDto<ProductDto>.Success(productDto, HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<ResponseDto<bool>> UpdateAsyncNew(ProductUpdateDto updateProduct)
        {
            var isSuccess = await _productRepository.UpdateAsyncNew(updateProduct);
            if (!isSuccess)
            {
                return ResponseDto<bool>.Fail(new List<string> { "işlem esnasında bir hata meydana geldi." }, HttpStatusCode.InternalServerError);
            }
            return ResponseDto<bool>.Success(true, HttpStatusCode.NoContent);
        }

        [HttpPost]
        public async Task<ResponseDto<bool>> UpsertAsyncNew(ProductUpsertDto request)
        {
            var isSuccess = await _productRepository.UpsertAsyncNew(request);
            if (!isSuccess)
            {
                return ResponseDto<bool>.Fail(new List<string> { "işlem esnasında bir hata meydana geldi." }, HttpStatusCode.InternalServerError);
            }
            return ResponseDto<bool>.Success(true, HttpStatusCode.NoContent);
        }

        public async Task<ResponseDto<bool>> DeleteAsyncNew(string id)
        {
            var deleteResponse = await _productRepository.DeleteAsyncNew(id);

            if (!deleteResponse.IsValidResponse && deleteResponse.Result == Elastic.Clients.Elasticsearch.Result.NotFound)
            {
                return ResponseDto<bool>.Fail(new List<string> { "Silmeye çalıştığınız ürün bulunamamıştır." }, HttpStatusCode.NotFound);
            }


            if (!deleteResponse.IsValidResponse)
            { 
                deleteResponse.TryGetOriginalException(out Exception exception);
                _logger.LogError(exception, deleteResponse.ElasticsearchServerError.Error.ToString());
                return ResponseDto<bool>.Fail(new List<string> { "işlem esnasında bir hata meydana geldi." }, HttpStatusCode.InternalServerError);
            }
            return ResponseDto<bool>.Success(true, HttpStatusCode.NoContent);
        }

        #endregion
    }
}
