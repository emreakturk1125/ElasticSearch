using ElasticSearch.Web.Models;
using ElasticSearch.Web.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElasticSearch.Web.Services
{
    public interface IECommerceService
    {
        Task<(List<ECommerceViewModel> list, long totalCount, long pageLinkCount)> SearchAsync(ECommerceSearchViewModel searchViewModel, int page, int pageSize);
    }
}
