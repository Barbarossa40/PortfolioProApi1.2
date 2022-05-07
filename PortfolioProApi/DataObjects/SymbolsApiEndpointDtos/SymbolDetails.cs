namespace PortfolioProApi.DataObjects.SymbolsApiEndpointDtos
{
    public class SymbolDetails
    {
        public int id { get; set; }
        public string? type { get; set; }

        public DetailAttributes attributes { get; set; }
    }
}
