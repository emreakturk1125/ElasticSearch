using ElasticSearch.API.Enum;

namespace ElasticSearch.API.DTOs
{
    public record ProductFeatureDto(int Width, int Height, EColor Color)
    {
    }
}
