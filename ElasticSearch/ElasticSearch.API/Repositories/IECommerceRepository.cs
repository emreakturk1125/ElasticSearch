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
        Task<ImmutableList<ECommerce>> PrefixQueryAsync(string customerFullName);
        Task<ImmutableList<ECommerce>> RangeQueryAsync(double fromPrice, double toPrice);
        Task<ImmutableList<ECommerce>> MatchAllQueryAsync();
        Task<ImmutableList<ECommerce>> PaginationQueryAsync(int page, int pageSize);
        Task<ImmutableList<ECommerce>> WilCardQueryAsync(string customerFullName);
        Task<ImmutableList<ECommerce>> FuzzyQueryAsync(string customerName);
        Task<ImmutableList<ECommerce>> MatchAllFullTextQueryAsync(string categoryName);
        Task<ImmutableList<ECommerce>> MatchAllFullTextQueryAndOperatorAsync(string categoryName);
        Task<ImmutableList<ECommerce>> MatchBoolPrefixFullTextQueryAsync(string categoryFullName);
        Task<ImmutableList<ECommerce>> MatchPhraseFullTextQueryAsync(string categoryFullName);
        Task<ImmutableList<ECommerce>> CompoundQueryExampleOneAsync(string cityName, double taxfulTotalPrice, string categoryName, string manufacturer);
        Task<ImmutableList<ECommerce>> CompoundQueryExampleTwoAsync(string customerFullName);
        Task<ImmutableList<ECommerce>> MultiMatchQueryExampleAsync(string name);
    }
}
