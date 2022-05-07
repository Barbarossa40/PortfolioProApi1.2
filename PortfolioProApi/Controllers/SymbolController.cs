using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortfolioProApi.DataObjects.SymbolsApiEndpointDtos;
using Flurl.Http;
using PortfolioProApi.DataObjects.SymbolChartEndpointDtos;

namespace PortfolioProApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SymbolController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _webApiOptions;
        private readonly string _apiKey;
        public SymbolController(IConfiguration configuration)
        {
            _configuration = configuration;
            _webApiOptions = _configuration.GetSection("WebApiOptions");
            _apiKey = _webApiOptions.GetSection("ApiKey").Value;
        }

        [HttpGet]
        public async Task<DetailAttributes> GetKeyDataBySymbol(string symbol)
        {

            DetailAttributes detailAttributes = new DetailAttributes();

            string apiUri = $"https://seeking-alpha.p.rapidapi.com/symbols/get-key-data?symbol={symbol}";
            var apiTask = await apiUri.WithHeaders(new
            {
                x_rapidapi_host = "seeking-alpha.p.rapidapi.com",
                x_rapidapi_key = _apiKey
            }).GetJsonAsync<Symbol>();

            detailAttributes = apiTask.data!.Select(ad => ad.attributes).First();


                return detailAttributes;

        }
        [HttpGet]
        [Route("name")]
        public async Task<string> GetName(string symbol)
        {

            DetailAttributes detailAttributes = new DetailAttributes();

            string apiUri = $"https://seeking-alpha.p.rapidapi.com/symbols/get-key-data?symbol={symbol}";
            var apiTask = await apiUri.WithHeaders(new
            {
                x_rapidapi_host = "seeking-alpha.p.rapidapi.com",
                x_rapidapi_key = _apiKey
            }).GetJsonAsync<Symbol>();

            detailAttributes = apiTask.data!.Select(ad => ad.attributes).First();
            
            string name = detailAttributes.companyName;

            return name;



        }


        [HttpGet]
        [Route("chart")]
        public async Task<Chart> GetChartDataBySymbol(string symbol, string period)
        { // get dailt data that we can chart. period is a string with ranges  1D|5D|1M|6M|1Y|5Y|10Y|MAX

            string apiUri = $"https://seeking-alpha.p.rapidapi.com/symbols/get-chart?symbol={symbol}&period={period}";
            var apiTask = await apiUri.WithHeaders(new
            {
                x_rapidapi_host = "seeking-alpha.p.rapidapi.com",
                x_rapidapi_key = _apiKey
            }).GetJsonAsync<Chart>();

            return apiTask;
        }



    }
}
