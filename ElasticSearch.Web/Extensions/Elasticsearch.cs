using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ElasticSearch.Web.Extensions
{
    public static class Elasticsearch
    {
        public static void AddElastic(this IServiceCollection services, IConfiguration configuration)
        {
            var username = configuration.GetSection("Elastic")["Username"];
            var password = configuration.GetSection("Elastic")["Password"];
            var settings = new ElasticsearchClientSettings(new System.Uri(configuration.GetSection("Elastic")["Url"]!)).Authentication(new BasicAuthentication(username!, password!));
            var client = new ElasticsearchClient(settings);
            services.AddSingleton(client);  
        }
    }
}
