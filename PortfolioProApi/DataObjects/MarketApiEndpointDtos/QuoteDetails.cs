namespace PortfolioProApi.DataObjects.MarketEndpointDtos
{
    public class QuoteDetails
    {

        public string indentifier { get; set; }
        public string name { get; set; }
        public double? last { get; set; }

        public double? transaction { get; set; }

        public double? percentTransaction { get; set; }
        public double? open { get; set; }
        public double? high { get; set; }
        public double? low { get; set; }
        public double? volume { get; set; }
        public string? dateTime { get; set; }
        public double? close { get; set; }

        public double? low52Week  { get; set; }
        public double? high52Week { get; set; }


    }
}
