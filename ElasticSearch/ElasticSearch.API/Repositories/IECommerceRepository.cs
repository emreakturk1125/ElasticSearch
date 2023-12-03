using ElasticSearch.API.Models;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace ElasticSearch.API.Repositories
{
    public interface IECommerceRepository
    {
        Task<ImmutableList<ECommerce>> TermQueryAsync(string customerFirstName);
        Task<ImmutableList<ECommerce>> TermsQueryListAsync(List<string> customerFirstNameList);
        Task<ImmutableList<ECommerce>> PrefixQueryAsync(string customerFirstName);
        Task<ImmutableList<ECommerce>> RangeQueryAsync(double fromPrice, double toPrice);
        Task<ImmutableList<ECommerce>> MatchAllQueryAsync();
        Task<ImmutableList<ECommerce>> PaginationQueryAsync(int page, int pageSize);
        Task<ImmutableList<ECommerce>> WilCardQueryAsync(string customerFullName);
    }
}
