namespace PortfolioProApi.CryptoDataObjects.CryptoSearch
{
    public class ResultDetails
    {

        public string uuid { get; set; }
        public string iconUrl { get; set; }
        public string name { get; set; }
        public string symbol { get; set; }
        public string price { get; set; }

        public string baseSymbol { get; set; }
        public string quoteSymbol { get; set; }
        public string baseUuid { get; set; }
        public string quoteUuid { get; set; }

        public string extransactionIconUrl { get; set; }
        public string extransactionName { get; set; }
        public string extransactionUuid { get; set; }
        public bool recommended { get; set; }
    }
}
