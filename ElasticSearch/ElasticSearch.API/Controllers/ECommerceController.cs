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

        /// <summary>
        /// customerFirstName  parametresinden gelen değere göre birebir arama yapar
        /// </summary>
        /// <param name="customerFirstName"></param>
        /// <returns></returns>
        [HttpGet("TermQueryAsync")]
        public async Task<IActionResult> TermQueryAsync(string customerFirstName)
        {
            return Ok(await _repository.TermQueryAsync(customerFirstName));      
        }

        /// <summary>
        /// customerFirstNameList listesindeki birden fazla gelen değerlere göre birebir arama yapar
        /// </summary>
        /// <param name="customerFirstNameList"></param>
        /// <returns></returns>
        [HttpPost("TermsQueryListAsync")]
        public async Task<IActionResult> TermsQueryListAsync(List<string> customerFirstNameList)
        {
            return Ok(await _repository.TermsQueryListAsync(customerFirstNameList));
        }

        /// <summary>
        /// İlk harfleri gelen parametre ile eşleşen değerleri getirir
        /// </summary>
        /// <param name="customerFullName"></param>
        /// <returns></returns>
        [HttpGet("PrefixQueryAsync")]
        public async Task<IActionResult> PrefixQueryAsyncAsync(string customerFullName)
        {
            return Ok(await _repository.PrefixQueryAsync(customerFullName));
        }

        /// <summary>
        /// belirli aralıktaki değerleri getirir
        /// </summary>
        /// <param name="fromPrice"></param>
        /// <param name="toPrice"></param>
        /// <returns></returns>
        [HttpGet("RangeQueryAsync")]
        public async Task<IActionResult> RangeQueryAsync(double fromPrice, double toPrice)
        {
            return Ok(await _repository.RangeQueryAsync(fromPrice,toPrice));
        }

        /// <summary>
        ///  Tanımlı size göre bütün verileri getirir
        /// </summary>
        /// <returns></returns>
        [HttpGet("MatchAllQueryAsync")]
        public async Task<IActionResult> MatchAllQueryAsync()
        {
            return Ok(await _repository.MatchAllQueryAsync());
        }

        /// <summary>
        /// Sayfalama işlemidir
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("PaginationQueryAsync")]
        public async Task<IActionResult> PaginationQueryAsync(int page = 1, int pageSize = 5)
        {
            return Ok(await _repository.PaginationQueryAsync(page,pageSize));
        }

        /// <summary>
        /// Sql like  komutuna benzer  örnek parametre;
        /// string customerFullName = "Elyysa*"
        /// string customerFullName = "*Elyysa*"
        /// string customerFullName = "Ely*",
        /// string customerFullName = "Elyy* Underwood",
        /// string customerFullName = "Elyy* Underwo*",
        /// her türlü like işlemi yapılabilir.
        /// Yıldız(*) olması kaydıyla
        /// </summary>
        /// <param name="customerFullName"></param>
        /// <returns></returns>
        [HttpGet("WilCardQueryAsync")]
        public async Task<IActionResult> WilCardQueryAsync(string customerFullName)
        {
            return Ok(await _repository.WilCardQueryAsync(customerFullName));
        }
    }
}
