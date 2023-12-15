using ElasticSearch.API.Models.ModelsForNestLibrary;
using Nest;
using System;

namespace ElasticSearch.API.DTOs
{
    public record ProductDto(string Id, string Name, decimal Price, int Stock, ProductFeatureDto Feature)
    { 
        
    }
}
