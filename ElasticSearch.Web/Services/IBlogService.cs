using ElasticSearch.Web.Models;
using ElasticSearch.Web.Repository;
using ElasticSearch.Web.ViewModel;
using System.Threading.Tasks;

namespace ElasticSearch.Web.Services
{
    public interface IBlogService
    {
        Task<bool> SaveAsync(BlogCreateViewModel newBlog);
    }
}
