namespace PortfolioProApi.DataObjects.SymbolChartEndpointDtos
{
    public class Chart
    {
        public string type { get; set; }

        public Dictionary <string, DateDataPointObj> attributes{ get; set; }

        public string id { get; set; }
    }
}
