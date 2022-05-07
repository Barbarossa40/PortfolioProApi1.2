using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortfolioProApi.CryptoDataObjects.GetCoinsDtos;
using Flurl.Http;
using PortfolioProApi;

namespace PortfolioProApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoinController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _webApiOptions;
        private readonly string _apiKey;
        public CoinController(IConfiguration configuration)
        {
            _configuration = configuration;
            _webApiOptions = _configuration.GetSection("WebApiOptions");
            _apiKey = _webApiOptions.GetSection("ApiKey").Value;
        }

        [HttpGet]
        public async Task<List<CoinDetailsDo>> GetCoinList( )
        {
            /// defaults to top 50 coins but there is a bunch of optional parameters we can include see documentation https://developers.coinranking.com/api/documentation/coins

            string apiUri = $"https://coinranking1.p.rapidapi.com/coins";
            var apiTask = await apiUri.WithHeaders(new
            {
                x_rapidapi_host = "coinranking1.p.rapidapi.com",
                x_rapidapi_key = _apiKey
            }).GetJsonAsync<CoinList>();


            return apiTask.data.coins;
        }

        [HttpGet]
        [Route("uuid")]
        public async Task<CoinDetailsDo> GetCoin(string uuid)
        { //  UUIDs of coins can be found using the Get coins endpoint or by checking the URL on coinranking.com, Ex: Qwsogvtv82FCd is btc
            /// can add params for timeperiod and reference currency. right now those are 24hrs and USD respectively  

            string apiUri = $"https://coinranking1.p.rapidapi.com/coin/{uuid}?referenceCurrencyUuid=yhjMzLPhuIDl&timePeriod=24h";
            var apiTask = await apiUri.WithHeaders(new
            {
                x_rapidapi_host = "coinranking1.p.rapidapi.com",
                x_rapidapi_key = _apiKey
            }).GetJsonAsync<CoinList>();
                
            return apiTask.data.coin;
        }

        [HttpGet]
        [Route("price")]
        public async Task<CoinPriceDetail> GetCoinCurrentPrice(string coinUuid)
        { //  UUIDs of coins can be found using the Get coins endpoint or by checking the URL on coinranking.com,  Ex: Qwsogvtv82FCd is btc yhjMzLPhuIDl is USD, razxDUgYGNAdQ is eth
            /// can add params for timeperiod and reference currency. right now those are 24hrs and USD respectively 

            string apiUri = $"https://coinranking1.p.rapidapi.com/coin/{coinUuid}/price?referenceCurrencyUuid=yhjMzLPhuIDl";
            var apiTask = await apiUri.WithHeaders(new
            {
                x_rapidapi_host = "coinranking1.p.rapidapi.com",
                x_rapidapi_key = _apiKey
            }).GetJsonAsync<CoinPrice>();


            return apiTask.data;
        }

        // add charting ability for coins. 2

        [HttpGet]
        [Route("chart")]
        public async Task<IEnumerable<CoinPriceDetail>> GetPriceHistory(string coinUuid, string timePeriod)
        { // UUID--
          // Ex: Qwsogvtv82FCd is btc yhjMzLPhuIDl is USD, razxDUgYGNAdQ is eth
            /// can add params for timeperiod and reference currency. right now those are 24hrs and USD respectively  time Allowed values: 3h 24h 7d 30d 3m 1y 3y 5y
          

            string apiUri = $"https://coinranking1.p.rapidapi.com/coin/{coinUuid}/history?referenceCurrencyUuid=yhjMzLPhuIDl&timePeriod={timePeriod}";
            var apiTask = await apiUri.WithHeaders(new
            {
                x_rapidapi_host = "coinranking1.p.rapidapi.com",
                x_rapidapi_key = _apiKey
            }).GetJsonAsync<CoinList>();


            return apiTask.data.history;
        }
    }
}
