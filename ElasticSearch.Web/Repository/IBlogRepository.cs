using ElasticSearch.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElasticSearch.Web.Repository
{
    public interface IBlogRepository
    {
        Task<Blog> SaveAsync(Blog newBlog);
        Task<List<Blog>> SearchAsync(string searchText);
        Task<List<Blog>> GetAll();
        Task<Blog> UpdateAsync(string blogId, Blog updatedBlog);
    }
}
