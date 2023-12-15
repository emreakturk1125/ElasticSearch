using ElasticSearch.API.Models.ModelsForNestLibrary;
using Nest;
using System;
using System.Threading.Tasks;

namespace ElasticSearch.API.Repositories
{
    public interface IProductRepository
    { 
        Task<Product> SaveAsync(Product newProduct); 
    }
}
