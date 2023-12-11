using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using ElasticSearch.Web.Models;
using ElasticSearch.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticSearch.Web.Repository
{
    public class ECommerceRepository : IECommerceRepository
    {
        private readonly ElasticsearchClient _elasticsearchClient;

        public ECommerceRepository(ElasticsearchClient elasticsearchClient)
        {
            _elasticsearchClient = elasticsearchClient;
        }

        private const string indexName = "kibana_sample_data_ecommerce";

        /// <summary>
        /// Çoklu arama ve pageging işlemlerinin olduğu sorgu
        /// </summary>
        /// <param name="searchViewModel"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<(List<ECommerce> list, long count)> SearchAsync(ECommerceSearchViewModel searchViewModel, int page, int pageSize)
        {

            List<Action<QueryDescriptor<ECommerce>>> listQuery = new();

            if (searchViewModel is null)
            {
                listQuery.Add(g => g.MatchAll());
            }
            else
            {
                if (!string.IsNullOrEmpty(searchViewModel.Category))
                {
                    listQuery.Add(q => q.Match(m => m.Field(f => f.Category).Query(searchViewModel.Category)));
                }
                if (!string.IsNullOrEmpty(searchViewModel.CustomerFullName))
                {
                    listQuery.Add(q => q.Match(m => m.Field(f => f.CustomerFullName).Query(searchViewModel.CustomerFullName)));
                }
                if (searchViewModel.OrderDateStart.HasValue)
                {
                    listQuery.Add(q => q.Range(r => r.DateRange(dr => dr.Field(f => f.OrderDate).Gte(searchViewModel.OrderDateStart))));
                }

                if (searchViewModel.OrderDateEnd.HasValue)
                {
                    listQuery.Add(q => q.Range(r => r.DateRange(dr => dr.Field(f => f.OrderDate).Lte(searchViewModel.OrderDateEnd))));
                }
                if (!string.IsNullOrEmpty(searchViewModel.Gender))
                {
                    listQuery.Add(q => q.Term(t => t.Field(f => f.Gender).Value(searchViewModel.Gender).CaseInsensitive()));
                }

                if (!listQuery.Any())
                {
                    listQuery.Add(g => g.MatchAll());
                }
            }

            return await CalculateResultSet(page, pageSize, listQuery);

        }

        private async Task<(List<ECommerce> list, long count)> CalculateResultSet(int page, int pageSize, List<Action<QueryDescriptor<ECommerce>>> listQuery)
        {
            var pageFrom = (page - 1) * pageSize;

            var result = await _elasticsearchClient.SearchAsync<ECommerce>(s => s.Index(indexName)
                                                                                    .Size(pageSize).From(pageFrom)
                                                                                    .Query(q => q
                                                                                          .Bool(b => b
                                                                                               .Must(listQuery.ToArray()))));

            foreach (var hit in result.Hits)
                hit.Source.Id = hit.Id;
            return (result.Documents.ToList(), result.Total);
        }
    }
}
