using ElasticSearch.API.Models;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace ElasticSearch.API.Repositories
{
    public interface IECommerceRepository
    {
        Task<ImmutableList<ECommerce>> TermQuery(string customerFirstName);
        Task<ImmutableList<ECommerce>> TermsQuery(List<string> customerFirstNameList);
    }
}
