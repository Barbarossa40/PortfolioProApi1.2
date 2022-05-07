using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Flurl.Http;
using PortfolioProApi.DataObjects.ArticlesEndpointApiDtos;

namespace PortfolioProApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _webApiOptions;
        private readonly string _apiKey;
        public ArticleController(IConfiguration configuration)
        {
            _configuration = configuration;
            _webApiOptions = _configuration.GetSection("WebApiOptions");
            _apiKey = _webApiOptions.GetSection("ApiKey").Value;
        }

        [HttpGet]
        public async Task<IEnumerable<ArticleDetails>> GetArticles()
        {
            // all paramters optional but mayube good to set timeconstraints and category
            //The Unix epoch is at the beginning of 1970 meaning that any timestamp prior to 1970 needs to be represented as a negative number representing the number of seconds until January 1st, 1970 00:00:00 UTC.
            // paramater time from/until is in unix time since equals the start of range andd until the end of search range
            // maybe just do datetime -	86400 Seconds (one day)
            string apiUri = $"https://seeking-alpha.p.rapidapi.com/articles/v2/list?until=0&since=0&size=20&number=1&category=latest-articles&since=0&size=20&number=1&category=etfs-and-funds";
            var apiTask = await apiUri.WithHeaders(new
            {
                x_rapidapi_host = "seeking-alpha.p.rapidapi.com",
                x_rapidapi_key = _apiKey
            }).GetJsonAsync<ArticleList>();


            return apiTask.data;
        }



        [HttpGet("{id}")]
        public async Task<ArticleDetails> GetArticleById(int id)
        {
            //need to add more fields to the models for the GetArticle details section. use 4349447 as example id number

            string apiUri = $"https://seeking-alpha.p.rapidapi.com/articles/get-details?id={id}";
            var apiTask = await apiUri.WithHeaders(new
            {
                x_rapidapi_host = "seeking-alpha.p.rapidapi.com",
                x_rapidapi_key = _apiKey
            }).GetJsonAsync<Article>();


            return apiTask.data;

        }

        [HttpGet]
        [Route("analysis")]
        public async Task<IEnumerable<ArticleDetails>> GetAnalysisList(string symbol)
        { // this is technically a diuffenert endpoint than article called analysis. but they have similar json models, parameters, and subject matter 

            string apiUri = $"https://seeking-alpha.p.rapidapi.com/analysis/v2/list?id={symbol}&until=0&since=0&size=20&number=1";
            var apiTask = await apiUri.WithHeaders(new
            {
                x_rapidapi_host = "seeking-alpha.p.rapidapi.com",
                x_rapidapi_key = _apiKey
            }).GetJsonAsync<ArticleList>();


            return apiTask.data;

        }

        [HttpGet]
        [Route("analysis/detail")]
        public async Task<ArticleDetails> GetAnalysisDetailsById(int id)
        { // this is technically a diuffenert endpoint than article called analysis. but they have similar json models, parameters, and subject matter ex id for test= 4341786

            string apiUri = $"https://seeking-alpha.p.rapidapi.com/analysis/v2/list?id={id}&until=0&since=0&size=20&number=1";
            var apiTask = await apiUri.WithHeaders(new
            {
                x_rapidapi_host = "seeking-alpha.p.rapidapi.com",
                x_rapidapi_key = _apiKey
            }).GetJsonAsync<Article>();


            return apiTask.data;

        }

    }

}
