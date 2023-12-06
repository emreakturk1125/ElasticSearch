using ElasticSearch.Web.Models;
using ElasticSearch.Web.Repository;
using ElasticSearch.Web.ViewModel;
using System;
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

        public async Task<bool> SaveAsync(BlogCreateViewModel model)
        {
            var newBlog = new Blog()
            {
                Title = model.Title,
                UserId = Guid.NewGuid(),
                Content = model.Content,
                Tags = model.Tags.ToArray()
            };
            var isCreated = await _repository.SaveAsync(newBlog);
            return isCreated != null;
        }
    }
}
