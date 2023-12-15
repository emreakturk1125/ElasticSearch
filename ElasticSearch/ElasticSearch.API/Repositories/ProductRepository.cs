﻿using ElasticSearch.API.Models.ModelsForNestLibrary;
using Nest;
using System;
using System.Threading.Tasks;

namespace ElasticSearch.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ElasticClient _client;

        public ProductRepository(ElasticClient client)
        {
            _client = client;
        }

        public async Task<Product> SaveAsync(Product newProduct)
        {
            newProduct.Created = DateTime.Now;
            var response = await _client.IndexAsync(newProduct, x => x.Index("products").Id(Guid.NewGuid().ToString()));

            if (!response.IsValid)
                return null;    

            newProduct.Id = response.Id;

            return newProduct;
        }
    }
}
