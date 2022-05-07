using Microsoft.AspNetCore.Mvc;
using PortfolioProApi.DataObjects.MarketEndpointDtos;
using Flurl.Http;



namespace PortfolioProApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuoteController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _webApiOptions;
        private readonly string _apiKey;
        public QuoteController(IConfiguration configuration)
        {
            _configuration = configuration;
            _webApiOptions = _configuration.GetSection("WebApiOptions");
            _apiKey = _webApiOptions.GetSection("ApiKey").Value;
        }

        // GET api/<ValuesController>/5
        [HttpGet]
        public async Task<IEnumerable<Quote>> GetQuotesBySymbol(string symbols)
        {
            //we can get multiple stock prices at once. query string jsut needs formt companycode%2companycode%2Ccompanycode etc...

            string apiUri = $"https://seeking-alpha.p.rapidapi.com/market/get-realtime-prices?symbols={symbols}";
            var apiTask = await apiUri.WithHeaders(new
            {
                x_rapidapi_host = "seeking-alpha.p.rapidapi.com",
                x_rapidapi_key = _apiKey
            }).GetJsonAsync<RealTimeQuote>();


            return apiTask.data;
        }

    }
}

