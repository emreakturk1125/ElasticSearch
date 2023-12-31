﻿using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using ElasticSearch.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Xml.Linq;

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

        public async Task<List<Blog>> GetAll()
        {
            var result = await _elasticsearchClient.SearchAsync<Blog>(s => s.Index(indexName).Size(100).Query(q => q.MatchAll()));

            foreach (var hit in result.Hits)
                hit.Source.Id = hit.Id;
            var list = result.Documents.ToList();
            return list;    
             
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

        public async Task<Blog> UpdateAsync(string blogId, Blog updatedBlog)
        { 
            blogId = "7OkbRIwB0mDAsZl_lEI5"; 
            var existingBlog = await _elasticsearchClient.GetAsync<Blog>(blogId, x => x.Index(indexName));

            if (existingBlog.Source == null)
            { 
                return null;
            } 
            existingBlog.Source.Title = "Emre Title";
            existingBlog.Source.Content = "Emre Content";
            existingBlog.Source.Tags = new string[] {"emre","test"};

             

            //var updateResponse =  _elasticsearchClient.UpdateAsync<Blog>(blogId, u => u.Index(indexName).Doc(existingBlog.Source));
             
            return null;
        }
         
        /// <summary>
        /// Match             => aranılacak kelimeyi birebir arar ve sonucu getirir
        /// MatchBoolPrefix   => aranılacak kelimenin ilk birkaç harfi yazılsa da sonucu getirir. 
        /// Should
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public async Task<List<Blog>> SearchAsync(string searchText)
        {

            var listQuery = new List<Action<QueryDescriptor<Blog>>>();

            Action<QueryDescriptor<Blog>> matchAll = (q) => q.MatchAll();

            Action<QueryDescriptor<Blog>> matchContent = (q) => q.Match(m => m
                                                                 .Field(f => f.Content)
                                                                 .Query(searchText));

            Action<QueryDescriptor<Blog>> matchTitle = (q) => q.MatchBoolPrefix(m => m
                                                               .Field(f => f.Title)
                                                               .Query(searchText));

            Action<QueryDescriptor<Blog>> matchTags = (q) => q.Term(t => t .Field(f => f.Tags).Value(searchText));    
            if (string.IsNullOrEmpty(searchText))
            {
                listQuery.Add(matchAll);
            }
            else
            {
                listQuery.Add(matchTitle);
                listQuery.Add(matchContent);
                listQuery.Add(matchTags); 
            }


            var result = await _elasticsearchClient.SearchAsync<Blog>(s => s.Index(indexName)
                                                                                    .Size(1000)
                                                                                    .Query(q => q
                                                                                          .Bool(b => b
                                                                                               .Should(listQuery.ToArray()))));

            foreach (var hit in result.Hits)
                hit.Source.Id = hit.Id;
            return result.Documents.ToList();
        }
    }
}
