using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortfolioProApi.DataObjects.SearchApiEndpointDtos;
using Flurl.Http;
using PortfolioProApi.DataObjects.SearchAuthorApiEndpointDtos;
using PortfolioProApi.CryptoDataObjects.CryptoSearch;

namespace PortfolioProApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _webApiOptions;
        private readonly string _apiKey;
        public SearchController(IConfiguration configuration)
        {
            _configuration = configuration;
            _webApiOptions = _configuration.GetSection("WebApiOptions");
            _apiKey = _webApiOptions.GetSection("ApiKey").Value;
        }
        [HttpGet]
        public async Task<SearchResult> GetSearchResult(string search, string type)
        {
            //we can serach in three areas  people,symbols,pages.  people%2csymbols%2cpages in the Q string but we can choose to include less ex: people%2csymbols
            // %2c representys a comma in a query string. 

            string apiUri = $"https://seeking-alpha.p.rapidapi.com/v2/auto-complete?query={search}&type={type}&size=5";
            var apiTask = await apiUri.WithHeaders(new
            {
                x_rapidapi_host = "seeking-alpha.p.rapidapi.com",
                x_rapidapi_key = _apiKey
            }).GetJsonAsync<SearchResult>();


            return apiTask;
        }

        [HttpGet("{slug}")]
       public async Task<AuthorDetails> GetAuthorDetailsById(string slug)
        {
            // slug is the term they use. It's under the people section of the search endpoint    ex:  apple-investor
            string apiUri = $"https://seeking-alpha.p.rapidapi.com/authors/get-details?slug={slug}";
            var apiTask = await apiUri.WithHeaders(new
            {
                x_rapidapi_host = "seeking-alpha.p.rapidapi.com",
                x_rapidapi_key = _apiKey
            }).GetJsonAsync<Author>();

            return apiTask.data;
        }

        [HttpGet]
        [Route("crypto")]
        public async Task<IActionResult> CryptoSearch(string query)
        {
            try
            {


                // query returns a coin or extransaction or market related to search
                string apiUri = $"https://coinranking1.p.rapidapi.com/search-suggestions?referenceCurrencyUuid=yhjMzLPhuIDl&query={query}";
                var apiTask = await apiUri.WithHeaders(new
                {
                    x_rapidapi_host = "coinranking1.p.rapidapi.com",
                    x_rapidapi_key = _apiKey
                }).GetJsonAsync<Search>();

                List<ResultDetails> results = apiTask.data.coins;

                if (results.Count > 0)
                {
                    return Ok(results);

                }
                else
                {
                    NoContent();
                }
            }catch(Exception ex)
            { 
                return BadRequest(ex.Message);
            
            }

            return  BadRequest();
        }
    }
}
