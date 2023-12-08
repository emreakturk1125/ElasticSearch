using ElasticSearch.Web.Models;
using ElasticSearch.Web.Repository;
using ElasticSearch.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElasticSearch.Web.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _repository;

        public BlogService(IBlogRepository blogRepository)
        {
            _repository = blogRepository;
        }

        public async Task<List<Blog>> GetAll()
        { 
            return await _repository.GetAll();
        }

        public async Task<bool> SaveAsync(BlogCreateViewModel model)
        {
            var newBlog = new Blog()
            {
                Title = model.Title,
                UserId = Guid.NewGuid(),
                Content = model.Content,
                Tags = model.Tags.Split(",")
            };
            var isCreated = await _repository.SaveAsync(newBlog);
            return isCreated != null;
        }

        public async Task<List<Blog>> SearchAsync(string searchText)
        {
            return await _repository.SearchAsync(searchText);   
        }
    }
}
