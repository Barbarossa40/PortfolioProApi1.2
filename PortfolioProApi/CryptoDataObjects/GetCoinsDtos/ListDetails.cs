namespace PortfolioProApi.CryptoDataObjects.GetCoinsDtos
{
    public class ListDetails
    {

        public int referenceCurrencyRate { get; set; }

        public int totalCoins { get; set; }

        public int totalMarkets { get; set; }
        public int totalExtransactions { get; set; }
        public string totalMarketCap { get; set; }
        public string total24hVolume { get; set; }
        public string btcDominance { get; set; }

        public List<CoinDetailsDo> bestCoins { get; set; }
        public List<CoinDetailsDo> newestCoins { get; set; }

        public List<CoinDetailsDo> coins { get; set; }

        public  CoinDetailsDo coin { get; set; }

        public List<CoinPriceDetail> history { get; set; }

    }
}
