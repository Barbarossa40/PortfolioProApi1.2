namespace PortfolioProApi.DataObjects.SymbolsApiEndpointDtos
{
    public class DetailAttributes
    {

        public string? slug { get; set; }
        public string? name { get; set; }

        public string? extransaction { get; set; }
        public string? companyName { get; set; }
        public string? equityType { get; set; }  

        //in trillions in the example need to see hoe this will look for smaller companies
        public double? bookValue { get; set; }
        //public double? cash { get; set; }
        //public double? debt { get; set; }


        //public double? divRate { get; set; }
        //public double? divYield { get; set; }
        //public double? divYield4y { get; set; }
        public double? marketCap { get; set; }
        public double? open { get; set; }
        //public double? shares { get; set; }
        //public double? divGrowRate { get; set; }
        //public string? lastDivDate { get; set; }
        public string? description { get; set; }
    }
}
