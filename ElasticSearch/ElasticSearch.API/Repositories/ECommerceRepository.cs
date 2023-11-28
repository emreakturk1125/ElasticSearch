using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Core.Search;
using Elastic.Clients.Elasticsearch.QueryDsl;
using ElasticSearch.API.Models;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace ElasticSearch.API.Repositories
{
    public class ECommerceRepository : IECommerceRepository
    {
        private readonly ElasticsearchClient _client;
        private const string indexName = "kibana_sample_data_ecommerce";

        public ECommerceRepository(ElasticsearchClient client)
        {
            _client = client;
        }

        public async Task<ImmutableList<ECommerce>> TermQuery(string customerFirstName)
        {
            #region ElasticSearch veri Çekme 1. Yol 
            //var result = await _client.SearchAsync<ECommerce>(s => s.Index(indexName).Query(q => q.Term(t => t.Field("customer_first_name.keyword").Value(customerFirstName))));
            #endregion

            #region ElasticSearch veri Çekme 2. Yol  
            // var result = await _client.SearchAsync<ECommerce>(s => s.Index(indexName).Query(q => q.Term(t => t.CustomerFirstName.Suffix("keyword"),customerFirstName)));
            #endregion

            #region ElasticSearch veri Çekme 3. Yol 
              
            var termQuery = new TermQuery("customer_first_name.keyword") { Value = customerFirstName, CaseInsensitive = true };
            var result = await _client.SearchAsync<ECommerce>(s => s.Index(indexName).Query(termQuery)); 

            #endregion

            foreach (var hit in result.Hits)
                hit.Source.Id = hit.Id;
            return result.Documents.ToImmutableList();
        }

        public async Task<ImmutableList<ECommerce>> TermsQuery(List<string> customerFirstNameList)
        {
            List<FieldValue> terms = new List<FieldValue>();

            customerFirstNameList.ForEach(x =>
            {
                terms.Add(x);
            });

            #region 1. Yol
            //var termsQuery = new TermsQuery()
            //{
            //    Field = "customer_first_name.keyword",
            //    Terms = new TermsQueryField(terms.AsReadOnly())
            //};
            //var result = await _client.SearchAsync<ECommerce>(s => s.Index(indexName).Query(termsQuery)); 
            #endregion

            #region 2. Yol

            var result = await _client.SearchAsync<ECommerce>(s => s.Index(indexName).Size(100)
            .Query(q => q
            .Terms(f=>f
            .Field(f => f.CustomerFirstName
            .Suffix("keyword"))
            .Terms(new TermsQueryField(terms.AsReadOnly())))));

            #endregion

            foreach (var hit in result.Hits)
                hit.Source.Id = hit.Id;
            return result.Documents.ToImmutableList();
        }
    }
}
