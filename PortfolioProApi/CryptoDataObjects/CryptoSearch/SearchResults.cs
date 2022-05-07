namespace PortfolioProApi.CryptoDataObjects.CryptoSearch
{
    public class SearchResults
    {

        public List<ResultDetails> coins { get; set; }

        public List<ResultDetails> extransactions { get; set; }

        public List<ResultDetails> markets { get; set; }
    }
}
