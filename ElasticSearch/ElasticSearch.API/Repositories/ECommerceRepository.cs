using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Core.Search;
using Elastic.Clients.Elasticsearch.QueryDsl;
using ElasticSearch.API.Models;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticSearch.API.Repositories
{
    public class ECommerceRepository : IECommerceRepository
    {
        private readonly ElasticsearchClient _client;
        private const string indexName = "kibana_sample_data_ecommerce";

        public ECommerceRepository(ElasticsearchClient client)
        {
            _client = client;
        }

        /// <summary>
        /// customerFirstName  parametresinden gelen değere göre birebir arama yapar
        /// </summary>
        /// <param name="customerFirstName"></param>
        /// <returns></returns>
        public async Task<ImmutableList<ECommerce>> TermQueryAsync(string customerFirstName)
        {
            #region ElasticSearch veri Çekme 1. Yol 
            //var result = await _client.SearchAsync<ECommerce>(s => s.Index(indexName).Query(q => q.Term(t => t.Field("customer_first_name.keyword").Value(customerFirstName))));
            #endregion

            #region ElasticSearch veri Çekme 2. Yol  
            // var result = await _client.SearchAsync<ECommerce>(s => s.Index(indexName).Query(q => q.Term(t => t.CustomerFirstName.Suffix("keyword"),customerFirstName)));
            #endregion

            #region ElasticSearch veri Çekme 3. Yol 

            var termQuery = new TermQuery("customer_first_name.keyword") { Value = customerFirstName, CaseInsensitive = true };
            var result = await _client.SearchAsync<ECommerce>(s => s.Index(indexName).Query(termQuery));

            #endregion

            foreach (var hit in result.Hits)
                hit.Source.Id = hit.Id;
            return result.Documents.ToImmutableList();
        }

        /// <summary>
        /// customerFirstNameList listesindeki birden fazla gelen değerlere göre birebir arama yapar
        /// </summary>
        /// <param name="customerFirstNameList"></param>
        /// <returns></returns>
        public async Task<ImmutableList<ECommerce>> TermsQueryListAsync(List<string> customerFirstNameList)
        {
            var terms = new List<FieldValue>();

            customerFirstNameList.ForEach(x =>
            {
                terms.Add(x);
            });

            #region 1. Yol
            //var termsQuery = new TermsQuery()
            //{
            //    Field = "customer_first_name.keyword",
            //    Terms = new TermsQueryField(terms.AsReadOnly())
            //};
            //var result = await _client.SearchAsync<ECommerce>(s => s.Index(indexName).Query(termsQuery)); 
            #endregion

            #region 2. Yol

            var result = await _client.SearchAsync<ECommerce>(s => s.Index(indexName).Size(100)
            .Query(q => q
            .Terms(f => f
            .Field(f => f.CustomerFirstName
            .Suffix("keyword"))
            .Terms(new TermsQueryField(terms.AsReadOnly())))));

            #endregion

            foreach (var hit in result.Hits)
                hit.Source.Id = hit.Id;
            return result.Documents.ToImmutableList();
        }

        /// <summary>
        /// İlk harfleri gelen parametre ile eşleşen değerleri getirir
        /// </summary>
        /// <param name="customerFullName"></param>
        /// <returns></returns>
        public async Task<ImmutableList<ECommerce>> PrefixQueryAsync(string customerFullName)
        {
            var result = await _client.SearchAsync<ECommerce>(s => s.Index(indexName).Size(20).Query(q => q.Prefix(p => p.Field(f => f.CustomerFullName.Suffix("keyword")).Value(customerFullName))));
            return result.Documents.ToImmutableList();
        }

        /// <summary>
        /// belirli aralıktaki değerleri getirir
        /// </summary>
        /// <param name="fromPrice"></param>
        /// <param name="toPrice"></param>
        /// <returns></returns>
        public async Task<ImmutableList<ECommerce>> RangeQueryAsync(double fromPrice, double toPrice)
        { 
            var result = await _client.SearchAsync<ECommerce>(s => s.Index(indexName).Size(20).Query(q => q.Range(r => r.NumberRange(nr => nr.Field(f => f.TaxfulTotalPrice).Gte(fromPrice).Lte(toPrice)))));
            return result.Documents.ToImmutableList();
        }

        /// <summary>
        ///  Tanımlı size göre bütün verileri getirir
        /// </summary>
        /// <returns></returns>
        public async Task<ImmutableList<ECommerce>> MatchAllQueryAsync()
        {
            var result = await _client.SearchAsync<ECommerce>(s => s.Index(indexName).Size(100).Query(q => q.MatchAll()));

            foreach (var hit in result.Hits)
                hit.Source.Id = hit.Id;
            return result.Documents.ToImmutableList();
        }

        /// <summary>
        /// Sayfalama işlemidir
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<ImmutableList<ECommerce>> PaginationQueryAsync(int page, int pageSize)
        {
            var pageFrom = (page - 1) * pageSize;

            var result = await _client.SearchAsync<ECommerce>(s => s.Index(indexName)
                                                                                   .Size(pageSize)
                                                                                   .From(pageFrom)
                                                                                   .Query(q => q.MatchAll()));

            foreach (var hit in result.Hits)
                hit.Source.Id = hit.Id;
            return result.Documents.ToImmutableList();
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
        public async Task<ImmutableList<ECommerce>> WilCardQueryAsync(string customerFullName)
        {
            var result = await _client.SearchAsync<ECommerce>(s => s.Index(indexName)
            .Query(q => q.Wildcard(w => w.Field(f => f.CustomerFullName.Suffix("keyword"))
            .Wildcard(customerFullName))));

            foreach (var hit in result.Hits)
                hit.Source.Id = hit.Id;
            return result.Documents.ToImmutableList();
        }

        /// <summary>
        /// Verilecek fuziness sayısına göre, aranacak kelimedeki harf hatasını tolere edebilir. 
        /// Örnek Fuziness sayısı : 1 ise aranacak kelimenin 1 harfi hatalı ya da eksik olursa yine veriyi getirir
        /// </summary>
        /// <param name="customerName"></param>
        /// <returns></returns>
        public async Task<ImmutableList<ECommerce>> FuzzyQueryAsync(string customerName)
        {
            var result = await _client.SearchAsync<ECommerce>(s => s.Index(indexName)
                                                                                    .Query(q => q.Fuzzy(fu => fu.Field(f => f.CustomerFirstName.Suffix("keyword"))
                                                                                    .Value(customerName)
                                                                                    .Fuzziness(new Fuzziness(2))))
                                                                                    .Sort(sort => sort.Field(f => f.TaxfulTotalPrice, new FieldSort() { Order = SortOrder.Desc })));

            foreach (var hit in result.Hits)
                hit.Source.Id = hit.Id;
            return result.Documents.ToImmutableList();
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
        public async Task<ImmutableList<ECommerce>> MatchAllFullTextQueryAsync(string categoryName)
        {

            var result = await _client.SearchAsync<ECommerce>(s => s.Index(indexName)
                                                                                    .Size(100)
                                                                                    .Query(q => q.Match(m => m.Field(f => f.Category).Query(categoryName))));

            foreach (var hit in result.Hits)
                hit.Source.Id = hit.Id;
            return result.Documents.ToImmutableList();
        }
         
        /// <summary>
        /// Üstteki metod or ifadesine göre arama yapıp sonuç getiriyordu. Bu metod ise and operatörü olduğu için aranacak kelimelerin aynı anda bulunduğu kayıtları getirir.
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        public async Task<ImmutableList<ECommerce>> MatchAllFullTextQueryAndOperatorAsync(string categoryName)
        {

            var result = await _client.SearchAsync<ECommerce>(s => s.Index(indexName)
                                                                                    .Size(100)
                                                                                    .Query(q => q.Match(m => m.Field(f => f.Category).Query(categoryName).Operator(Operator.And))));

            foreach (var hit in result.Hits)
                hit.Source.Id = hit.Id;
            return result.Documents.ToImmutableList();
        }
         
        /// <summary>
        /// 
        /// Örnek olarak "Fatih Yılmaz Güneş" şeklinde arama yapılsın
        /// fatih or yılmaz or (g.. or gü.. or gün..) şeklinde olanları getirir. En sonraki kelimeyi prefix olarak algılar
        /// 
        /// "Fatih Yılmaz" şeklinde arama yapılsın
        /// fatih or (y.. or yı..) şeklinde olanları getirir. En sonraki kelimeyi prefix olarak algılar
        /// 
        /// </summary>
        /// <param name="categoryFullName"></param>
        /// <returns></returns>
        public async Task<ImmutableList<ECommerce>> MatchBoolPrefixFullTextQueryAsync(string categoryFullName)
        {

            var result = await _client.SearchAsync<ECommerce>(s => s.Index(indexName)
                                                                                    .Size(100)
                                                                                    .Query(q => q.MatchBoolPrefix(m => m.Field(f => f.CustomerFullName).Query(categoryFullName))));

            foreach (var hit in result.Hits)
                hit.Source.Id = hit.Id;
            return result.Documents.ToImmutableList();
        }

        /// <summary>
        /// Bu aramada örnke olarak "Sultan Al Pratt" şeklinde arama yapıldığında öbek olarak arar. Öncesinde ya da sonrasında ne geldiği önemli değildir
        /// "Sultan Al"  böyle sıralı şekilde bir veri varsa, Öncesinde ya da sonrasında ne geldiği önemli değildir 
        /// "Sultan"
        /// </summary>
        /// <param name="categoryFullName"></param>
        /// <returns></returns>
        public async Task<ImmutableList<ECommerce>> MatchPhraseFullTextQueryAsync(string categoryFullName)
        {

            var result = await _client.SearchAsync<ECommerce>(s => s.Index(indexName)
                                                                                    .Size(100)
                                                                                    .Query(q => q.MatchPhrase(m => m.Field(f => f.CustomerFullName).Query(categoryFullName))));

            foreach (var hit in result.Hits)
                hit.Source.Id = hit.Id;
            return result.Documents.ToImmutableList();
        }

        /// <summary>
        /// Aranacak verinin; birden fazla field da full text olarak, yani girilen textin birebir aranması işlemidir
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<ImmutableList<ECommerce>> MultiMatchQueryExampleAsync(string name)
        {
            var result = await _client.SearchAsync<ECommerce>(s => s.Index(indexName)
                                                                                   .Size(100)
                                                                                   .Query(q => q
                                                                                         .MultiMatch(m => m
                                                                                                    .Fields(new Field("customer_first_name")
                                                                                                    .And(new Field("customer_last_name"))
                                                                                                    .And(new Field("customer_full_name")))
                                                                                                    .Query(name))));

            foreach (var hit in result.Hits)
                hit.Source.Id = hit.Id;
            return result.Documents.ToImmutableList();
        }

        /// <summary>
        /// Çoklu şart ifadeleri, çoklu arama işlemleri için
        /// </summary>
        /// <param name="cityName"></param>
        /// <param name="taxfulTotalPrice"></param>
        /// <param name="categoryName"></param>
        /// <param name="manufacturer"></param>
        /// <returns></returns>
        public async Task<ImmutableList<ECommerce>> CompoundQueryExampleOneAsync(string cityName, double taxfulTotalPrice, string categoryName, string manufacturer)
        {

            var result = await _client.SearchAsync<ECommerce>(s => s.Index(indexName)
                                                                                    .Size(100)
                                                                                    .Query(q => q
                                                                                          .Bool(b => b
                                                                                               .Must(m => m
                                                                                                    .Term(t => t
                                                                                                           .Field("geoip.city_name")                   //=>  bu şekilde de yazılabilir eğer arama yapacağın değer "geoip" gelmiyorsa
                                                                                                           .Value(cityName)))
                                                                                               .MustNot(mn => mn
                                                                                                       .Range(r => r
                                                                                                             .NumberRange(nr => nr
                                                                                                                         .Field(f => f.TaxfulTotalPrice).Lte(taxfulTotalPrice))))
                                                                                               .Should(s => s
                                                                                                      .Term(t => t
                                                                                                           .Field(f => f.Category.Suffix("keyword"))   //=>  bu şekilde de yazılabilir eğer arama yapacağın değer "Category" geliyorsa
                                                                                                           .Value(categoryName)))
                                                                                               .Filter(f => f
                                                                                                      .Term(t => t
                                                                                                           .Field("manufacturer.keyword")
                                                                                                           .Value(manufacturer))))));
                                                                                                      
            foreach (var hit in result.Hits)
                hit.Source.Id = hit.Id;
            return result.Documents.ToImmutableList();
        }

        /// <summary>
        /// Aranacak kelime "Emre Aktürk" olarak varsayalım
        /// "Emre Aktürk" böyle de arama sonuçlarını getirir
        /// "Emre Aktü.." böyle de arama sonuçlarını getirir
        /// "Emre" böyle de arama sonuçlarını getirir
        /// "Em.." böyle de arama sonuçlarını getirir
        /// </summary>
        /// <param name="customerFullName"></param>
        /// <returns></returns>
        public async Task<ImmutableList<ECommerce>> CompoundQueryExampleTwoAsync(string customerFullName)
        {
            
            //1. Yol
            //var result = await _client.SearchAsync<ECommerce>(s => s.Index(indexName)
            //                                                                      .Size(100)
            //                                                                      .Query(q => q
            //                                                                            .MatchPhrasePrefix(m => m.Field(f => f.CustomerFullName).Query(customerFullName))));



            // 2. yol
            var result = await _client.SearchAsync<ECommerce>(s => s.Index(indexName)
                                                                                    .Size(100)
                                                                                    .Query(q => q
                                                                                          .Bool(b => b
                                                                                               .Should(s => s
                                                                                                    .Match(m => m
                                                                                                           .Field(f => f.CustomerFullName)
                                                                                                           .Query(customerFullName))
                                                                                                    .Prefix(p => p
                                                                                                           .Field(f => f.CustomerFullName.Suffix("keyword"))
                                                                                                           .Value(customerFullName))))));

            foreach (var hit in result.Hits)
                hit.Source.Id = hit.Id;
            return result.Documents.ToImmutableList();
        }
         
    }
}
