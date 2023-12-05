using ElasticSearch.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElasticSearch.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ECommerceController : ControllerBase
    {
        private readonly IECommerceRepository _repository;

        public ECommerceController(IECommerceRepository repository)
        {
            _repository = repository;
        }
         
        [HttpGet("TermQueryAsync")]
        public async Task<IActionResult> TermQueryAsync(string customerFirstName)
        {
            return Ok(await _repository.TermQueryAsync(customerFirstName));      
        }
         
        [HttpPost("TermsQueryListAsync")]
        public async Task<IActionResult> TermsQueryListAsync(List<string> customerFirstNameList)
        {
            return Ok(await _repository.TermsQueryListAsync(customerFirstNameList));
        }
         
        [HttpGet("PrefixQueryAsync")]
        public async Task<IActionResult> PrefixQueryAsyncAsync(string customerFullName)
        {
            return Ok(await _repository.PrefixQueryAsync(customerFullName));
        }
         
        [HttpGet("RangeQueryAsync")]
        public async Task<IActionResult> RangeQueryAsync(double fromPrice, double toPrice)
        {
            return Ok(await _repository.RangeQueryAsync(fromPrice,toPrice));
        }
         
        [HttpGet("MatchAllQueryAsync")]
        public async Task<IActionResult> MatchAllQueryAsync()
        {
            return Ok(await _repository.MatchAllQueryAsync());
        }
         
        [HttpGet("PaginationQueryAsync")]
        public async Task<IActionResult> PaginationQueryAsync(int page = 1, int pageSize = 5)
        {
            return Ok(await _repository.PaginationQueryAsync(page,pageSize));
        }
         
        [HttpGet("WilCardQueryAsync")]
        public async Task<IActionResult> WilCardQueryAsync(string customerFullName)
        {
            return Ok(await _repository.WilCardQueryAsync(customerFullName));
        }
         
        [HttpGet("FuzzyQueryAsync")]
        public async Task<IActionResult> FuzzyQueryAsync(string customerName)
        {
            return Ok(await _repository.FuzzyQueryAsync(customerName));
        }
         
        [HttpGet("MatchAllFullTextQueryAsync")]
        public async Task<IActionResult> MatchAllFullTextQueryAsync(string categoryName)
        {
            return Ok(await _repository.MatchAllFullTextQueryAsync(categoryName));
        }
         
        [HttpGet("MatchAllFullTextQueryAndOperatorAsync")]
        public async Task<IActionResult> MatchAllFullTextQueryAndOperatorAsync(string categoryName)
        {
            return Ok(await _repository.MatchAllFullTextQueryAndOperatorAsync(categoryName));
        }
         
        [HttpGet("MatchBoolPrefixFullTextQueryAsync")]
        public async Task<IActionResult> MatchBoolPrefixFullTextQueryAsync(string categoryFullName)
        {
            return Ok(await _repository.MatchBoolPrefixFullTextQueryAsync(categoryFullName));
        }

        [HttpGet("MatchPhraseFullTextQueryAsync")]
        public async Task<IActionResult> MatchPhraseFullTextQueryAsync(string categoryFullName)
        {
            return Ok(await _repository.MatchPhraseFullTextQueryAsync(categoryFullName));
        }

        [HttpGet("MultiMatchQueryExampleAsync")]
        public async Task<IActionResult> MultiMatchQueryExampleAsync(string name)
        {
            return Ok(await _repository.MultiMatchQueryExampleAsync(name));
        }

        [HttpGet("CompoundQueryExampleOneAsync")]
        public async Task<IActionResult> CompoundQueryExampleOneAsync(string cityName,double taxfulTotalPrice,string categoryName,string manufacturer)
        {
            return Ok(await _repository.CompoundQueryExampleOneAsync(cityName,taxfulTotalPrice,categoryName,manufacturer));
        }

        [HttpGet("CompoundQueryExampleTwoAsync")]
        public async Task<IActionResult> CompoundQueryExampleTwoAsync(string customerFullName)
        {
            return Ok(await _repository.CompoundQueryExampleTwoAsync(customerFullName));
        }
         
    }
}
 