using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Core.Search;
using ElasticSearch.API.Models;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace ElasticSearch.API.Repositories
{
    public class ECommerceRepository
    {
        private readonly ElasticsearchClient _client;
        private const string indexName = "kibana_sample_data_ecommerce";

        public ECommerceRepository(ElasticsearchClient client)
        {
            _client = client;
        }

        public async Task<ImmutableList<ECommerce>> TermQuery(string customerFirstName)
        {
           var result = await _client.SearchAsync<ECommerce>(s => s.Index(indexName).Query(q => q.Term(t => t.Field("customer_first_name.keyword").Value(customerFirstName))));
            foreach (var hit in result.Hits) 
                hit.Source.Id = hit.Id; 
           return  result.Documents.ToImmutableList();
        }
    }
}
