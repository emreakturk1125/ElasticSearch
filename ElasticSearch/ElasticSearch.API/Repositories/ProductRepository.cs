using Elastic.Clients.Elasticsearch;
using ElasticSearch.API.DTOs;
using ElasticSearch.API.Models.ModelsForNestLibrary;
using Nest;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace ElasticSearch.API.Repositories
{

    // Elastic search işlemleri için 2 farklı kütüphane ile aynı işlemler yapıldı
    public class ProductRepository : IProductRepository
    {
        private readonly ElasticsearchClient _elasticsearchClient;
        private readonly ElasticClient _client;
        private const string indexNameForNest = "products";
        private const string indexNameForElastic = "productsnew";

        public ProductRepository(ElasticClient client, ElasticsearchClient elasticsearchClient)
        {
            _client = client;
            _elasticsearchClient = elasticsearchClient; 
        }

        #region NEST KÜTÜPHANESİ
        public async Task<Product> SaveAsync(Product newProduct)
        {
            newProduct.Created = DateTime.Now;
            var response = await _client.IndexAsync(newProduct, x => x.Index(indexNameForNest).Id(Guid.NewGuid().ToString()));

            if (!response.IsValid)
                return null;

            newProduct.Id = response.Id;

            return newProduct;
        }

        public async Task<IImmutableList<Product>> GetAllAsync()
        {
            var response = await _client.SearchAsync<Product>(x => x.Index(indexNameForNest).Query(q => q.MatchAll()));

            foreach (var hit in response.Hits)
                hit.Source.Id = hit.Id;

            return response.Documents.ToImmutableList();
        }

        public async Task<Product> GetByIdAsync(string id)
        {
            var response = await _client.GetAsync<Product>(id, x => x.Index(indexNameForNest));

            if (!response.IsValid)
            {
                return null;
            }

            response.Source.Id = response.Id;

            return response.Source;
        }

        public async Task<bool> UpdateAsync(ProductUpdateDto updateProduct)
        {
            var response = await _client.UpdateAsync<Product, ProductUpdateDto>(updateProduct.Id, x => x.Index(indexNameForNest).Doc(updateProduct));
            return response.IsValid;
        }

        public async Task<bool> UpsertAsync(ProductUpsertDto product)
        {
            var response = await _client.UpdateAsync<Product, ProductUpsertDto>(product.Id, x => x.Index(indexNameForNest).DocAsUpsert(true).Doc(product));
            return response.IsValid;
        }

        public async Task<Nest.DeleteResponse> DeleteAsync(string id)
        {
            var response = await _client.DeleteAsync<Product>(id, x => x.Index(indexNameForNest));
            return response;
        }
        #endregion

        #region ELASTIC.CLIENTS.ELASTICSEARCH KÜTÜPHANESİ


        public async Task<Product> SaveAsyncNew(Product newProduct)
        {
            newProduct.Created = DateTime.Now;
            var response = await _elasticsearchClient.IndexAsync(newProduct, x => x.Index(indexNameForElastic).Id(Guid.NewGuid().ToString()));

            if (!response.IsValidResponse)
                return null;

            newProduct.Id = response.Id;

            return newProduct;
        }

        public async Task<IImmutableList<Product>> GetAllAsyncNew()
        {
            var response = await _elasticsearchClient.SearchAsync<Product>(x => x.Index(indexNameForElastic).Query(q => q.MatchAll()));

            foreach (var hit in response.Hits)
                hit.Source.Id = hit.Id;

            return response.Documents.ToImmutableList();
        }

        public async Task<Product> GetByIdAsyncNew(string id)
        {
            var response = await _elasticsearchClient.GetAsync<Product>(id, x => x.Index(indexNameForElastic));

            if (!response.IsValidResponse)
            {
                return null;
            }

            response.Source.Id = response.Id;

            return response.Source;
        }

        public async Task<bool> UpdateAsyncNew(ProductUpdateDto updateProduct)
        {
            var response = await _elasticsearchClient.UpdateAsync<Product, ProductUpdateDto>(indexNameForElastic,updateProduct.Id,x => x.Doc(updateProduct));
            return response.IsValidResponse;
        }

        public async Task<bool> UpsertAsyncNew(ProductUpsertDto product)
        {
            var response = await _elasticsearchClient.UpdateAsync<Product, ProductUpsertDto>(indexNameForElastic,product.Id, x => x.Index(indexNameForElastic).DocAsUpsert(true).Doc(product));
            return response.IsValidResponse;
        }

        public async Task<Elastic.Clients.Elasticsearch.DeleteResponse> DeleteAsyncNew(string id)
        {
            var response = await _elasticsearchClient.DeleteAsync<Product>(id, x => x.Index(indexNameForElastic));
            return response;
        }


        #endregion

    }
}
