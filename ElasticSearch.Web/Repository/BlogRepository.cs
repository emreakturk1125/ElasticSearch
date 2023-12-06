using Elastic.Clients.Elasticsearch;
using ElasticSearch.Web.Models;
using System;
using System.Threading.Tasks;

namespace ElasticSearch.Web.Repository
{
    public class BlogRepository : IBlogRepository
    {
        private readonly ElasticsearchClient _elasticsearchClient; 
        private const string indexName = "blog_index";

        public BlogRepository(ElasticsearchClient elasticsearchClient)
        {
            _elasticsearchClient = elasticsearchClient;
        }

        public async Task<Blog> SaveAsync(Blog newBlog)
        {
            newBlog.Created = DateTime.Now;
            var response = await _elasticsearchClient.IndexAsync(newBlog, x => x.Index(indexName));
            if (!response.IsValidResponse)
                return null;

            newBlog.Id = response.Id;
            return newBlog;
        }
    }
}
