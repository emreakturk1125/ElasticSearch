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

        /// <summary>
        /// Verilecek fuziness sayısına göre, aranacak kelimedeki harf hatasını tolere edebilir. 
        /// Örnek Fuziness sayısı : 1 ise aranacak kelimenin 1 harfi hatalı ya da eksik olursa yine veriyi getirir
        /// </summary>
        /// <param name="customerName"></param>
        /// <returns></returns>
        [HttpGet("FuzzyQueryAsync")]
        public async Task<IActionResult> FuzzyQueryAsync(string customerName)
        {
            return Ok(await _repository.FuzzyQueryAsync(customerName));
        }


        /// <summary> 
        /// Tüm verinin her bir kelimesi indexlenir aranırken kelime tam olarak yazılmalıdır. Kelimelerin eşlemşmesi lazım, eksik yazılırsa sonuç dönmez
        /// Skor değeri de döner (TermQuery lerde skor değeri yoktur çünkü birebir kelimeyi arar, şarta bağlı getirdği için skor değeri yoktur)
        /// Skor değeri aranan kelime ile en yakın ve benzer verilerin skor değeri yüksektir. Benzerlik azaldıkça skor değeri düşer
        /// FullTextQuery de keyword ifadesi kullanılmıyor
        /// TermQuery de kullanılıyor. keyword olduğu zaman tam olarak yazılan kelimeyi arar. Yani "Emre Aktürk" yazarsan "Emre Aktürk" olarak arar. "Emre" yazarsan "Emre" olarak arar
        /// FulltextQuery de keyword olmadığı için "Emre Aktürk"  olarak bir kelime girdiğiniz zaman "Emre" ya da "Aktürk" olarak or ifadesine göre arar.
        /// </summary>
        /// <param name="customerName"></param>
        /// <returns></returns>
        [HttpGet("MatchAllFullTextQueryAsync")]
        public async Task<IActionResult> MatchAllFullTextQueryAsync(string categoryName)
        {
            return Ok(await _repository.MatchAllFullTextQueryAsync(categoryName));
        }


        /// <summary>
        /// Üstteki metod or ifadesine göre arama yapıp sonuç getiriyordu. Bu metod ise and operatörü olduğu için aranacak kelimelerin aynı anda bulunduğu kayıtları getirir.
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        [HttpGet("MatchAllFullTextQueryAndOperatorAsync")]
        public async Task<IActionResult> MatchAllFullTextQueryAndOperatorAsync(string categoryName)
        {
            return Ok(await _repository.MatchAllFullTextQueryAndOperatorAsync(categoryName));
        }
    }
}
 