using ElasticSearch.API.DTOs;
using ElasticSearch.API.Models.ModelsForNestLibrary;
using Nest;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace ElasticSearch.API.Repositories
{
    public interface IProductRepository
    { 
        // NEST KÜTÜPHANESİ - START
        Task<Product> SaveAsync(Product newProduct);
        Task<IImmutableList<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(string id);
        Task<bool> UpdateAsync(ProductUpdateDto updateProduct);
        Task<bool> UpsertAsync(ProductUpsertDto product);
        Task<Nest.DeleteResponse> DeleteAsync(string id);
        // NEST KÜTÜPHANESİ - END

        // ----------------------------------------------------------------------------------------

        // ELASTIC.CLIENTS.ELASTICSEARCH KÜTÜPHANESİ - START
        Task<Product> SaveAsyncNew(Product newProduct);
        Task<IImmutableList<Product>> GetAllAsyncNew();
        Task<Product> GetByIdAsyncNew(string id);
        Task<bool> UpdateAsyncNew(ProductUpdateDto updateProduct);
        Task<bool> UpsertAsyncNew(ProductUpsertDto product);
        Task<Elastic.Clients.Elasticsearch.DeleteResponse> DeleteAsyncNew(string id);

        // ELASTIC.CLIENTS.ELASTICSEARCH KÜTÜPHANESİ - END
    }
}
