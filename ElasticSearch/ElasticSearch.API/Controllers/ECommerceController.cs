using ElasticSearch.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ElasticSearch.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ECommerceController : ControllerBase
    {
        private readonly ECommerceRepository _repository;

        public ECommerceController(ECommerceRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> TermQuery(string customerFirstName)
        {
            return Ok(await _repository.TermQuery(customerFirstName));      
        }
    }
}
