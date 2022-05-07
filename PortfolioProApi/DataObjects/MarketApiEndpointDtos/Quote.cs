
namespace PortfolioProApi.DataObjects.MarketEndpointDtos
{
    public class Quote
    {

        public string id { get; set; }
        public  string? type { get; set; }

        public QuoteDetails attributes { get; set; }


    }
}
