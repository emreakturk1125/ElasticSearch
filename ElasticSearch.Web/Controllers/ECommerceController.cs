using ElasticSearch.Web.Services;
using ElasticSearch.Web.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ElasticSearch.Web.Controllers
{
    public class ECommerceController : Controller
    {
        private readonly IECommerceService _eCommerceService;
        public ECommerceController(IECommerceService eCommerceService)
        {
            _eCommerceService = eCommerceService;
        }

        public async Task<IActionResult> Search([FromQuery] SearchPageViewModel searchViewPage)
        {
            var (eCommercelist,totalCount,pageLinkCount) = await _eCommerceService.SearchAsync(searchViewPage.SearchViewModel, searchViewPage.Page, searchViewPage.PageSize);
            searchViewPage.List = eCommercelist;
            searchViewPage.TotalCount = totalCount;
            searchViewPage.PageLinkCount = pageLinkCount;
            return View(searchViewPage);
        }
    }
}
