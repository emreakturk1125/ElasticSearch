using Elasticsearch.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace ElasticSearch.API.Extensions.NestLibrary
{
    public static class ElasticsearchExtension
    {
        public static void AddElasticNEST(this IServiceCollection services, IConfiguration configuration)
        {
            var pool = new SingleNodeConnectionPool(new System.Uri(configuration.GetSection("Elastic")["Url"]));
            var settings = new ConnectionSettings(pool);
            var client = new ElasticClient(settings);
            services.AddSingleton(client);
        }
    }
}
