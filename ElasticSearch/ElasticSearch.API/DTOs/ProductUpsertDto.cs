using Elastic.Clients.Elasticsearch;
using ElasticSearch.API.Enum;
using ElasticSearch.API.Models.ModelsForNestLibrary;

namespace ElasticSearch.API.DTOs
{
    public record ProductUpsertDto(string Id,string Name, decimal Price, int Stock, ProductFeatureDto Feature)
    {
       
    }
}
