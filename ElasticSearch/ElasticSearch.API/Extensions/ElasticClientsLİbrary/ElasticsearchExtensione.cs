using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ElasticSearch.API.Extensions.ElasticClientsLİbrary
{
    public static class ElasticsearchExtensione
    {
        public static void AddElasticELASTIC(this IServiceCollection services, IConfiguration configuration)
        {
            var username = configuration.GetSection("Elastic")["Username"];
            var password = configuration.GetSection("Elastic")["Password"];
            var settings = new ElasticsearchClientSettings(new Uri(configuration.GetSection("Elastic")["Url"])).Authentication(new BasicAuthentication(username, password));
            var client = new ElasticsearchClient(settings);
            services.AddSingleton(client);
        }
    }
}
