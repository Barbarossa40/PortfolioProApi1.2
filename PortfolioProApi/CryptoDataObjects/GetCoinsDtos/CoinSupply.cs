namespace PortfolioProApi.CryptoDataObjects.GetCoinsDtos
{
    public class CoinSupply
    {
        public bool confirmed { get; set; }
        public string total { get; set; }

        public string circulating { get; set; }
    }
}
