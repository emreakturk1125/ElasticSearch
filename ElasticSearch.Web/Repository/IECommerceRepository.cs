using ElasticSearch.Web.Models;
using ElasticSearch.Web.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElasticSearch.Web.Repository
{
    public interface IECommerceRepository
    {
        Task<(List<ECommerce> list, long count)> SearchAsync(ECommerceSearchViewModel searchViewModel, int page, int pageSize);
    }
}
