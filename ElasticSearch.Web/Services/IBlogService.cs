using ElasticSearch.Web.Models;
using ElasticSearch.Web.Repository;
using ElasticSearch.Web.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElasticSearch.Web.Services
{
    public interface IBlogService
    {
        Task<bool> SaveAsync(BlogCreateViewModel model);
        Task<List<Blog>> SearchAsync(string searchText);
        Task<List<Blog>> GetAll();
    }
}
