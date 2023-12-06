using ElasticSearch.Web.Models;
using System.Threading.Tasks;

namespace ElasticSearch.Web.Repository
{
    public interface IBlogRepository
    {
        Task<Blog> SaveAsync(Blog newBlog);
    }
}
