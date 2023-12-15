using ElasticSearch.Web.Models;
using ElasticSearch.Web.Repository;
using ElasticSearch.Web.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticSearch.Web.Services
{
    public class ECommerceService : IECommerceService
    {
        private readonly IECommerceRepository _repository;
        public ECommerceService(IECommerceRepository repository)
        {
            _repository = repository;
        }

        public async Task<(List<ECommerceViewModel> list, long totalCount, long pageLinkCount)> SearchAsync(ECommerceSearchViewModel searchViewModel, int page, int pageSize)
        {
            var (eCommerList, totalCount) = await _repository.SearchAsync(searchViewModel, page, pageSize);

            var pageLinkCountCalculate = totalCount % pageSize;
            long pageLinkCount = 0;

            if (pageLinkCountCalculate == 0)
                pageLinkCount = totalCount / pageSize;
            else
                pageLinkCount = (totalCount / pageSize) + 1;

            var eCommerListViewModel = eCommerList.Select(x => new ECommerceViewModel()
            {
                Id = x.Id,
                OrderId = x.OrderId,
                Category = string.Join(",",x.Category),
                CustomerFirstName = x.CustomerFirstName,
                CustomerLastName = x.CustomerLastName,
                CustomerFullName = x.CustomerFullName,
                OrderDate = x.OrderDate.ToShortDateString(),
                TaxfulTotalPrice = x.TaxfulTotalPrice,  
                Gender = x.Gender.ToLower(),
            }).ToList();

            return (eCommerListViewModel, totalCount, pageLinkCount);
        }
    }
}
