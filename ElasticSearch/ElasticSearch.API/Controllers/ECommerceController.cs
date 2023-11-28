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

        [HttpGet]
        public async Task<IActionResult> TermQuery(string customerFirstName)
        {
            return Ok(await _repository.TermQuery(customerFirstName));      
        }

        [HttpPost]
        public async Task<IActionResult> TermsQuery(List<string> customerFirstNameList)
        {
            return Ok(await _repository.TermsQuery(customerFirstNameList));
        }
    }
}
