namespace PortfolioProApi.DataObjects.SearchApiEndpointDtos
{
    public class SearchResult
    {

        public List<Person> people { get; set; }

        public List<Symbol> symbols { get; set; }

        public List<Page> pages { get; set; }
    }
}
