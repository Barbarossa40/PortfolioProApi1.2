using System.Text.Json.Serialization;

namespace PortfolioProApi.CryptoDataObjects.GetCoinsDtos
{
    public class CoinDetailsDo
    {
        public string uuid { get; set; }

        public string symbol { get; set; }

        public string name { get; set; }

        public string? description { get; set; }

        public string iconUrl { get; set; }

        public string websiteUrl { get; set; }

        public List<LinkItem> links { get; set; }

        public CoinSupply supply { get; set; }

        public int numberOfMarkets { get; set; }

        public int numberOfExtransactions { get; set; }

        public string? coinrankingUrl { get; set; }

        public string? marketCap { get; set; }

        public string? price { get; set; }

        public int? listedAt { get; set; }

        public int? tier { get; set; }

        public string? transaction { get; set; }

        public int? rank { get; set; }

        public bool? lowVolume { get; set; }


        [JsonPropertyName("24Volume")]
        public string? volume { get; set; }


        public string? btcPrice { get; set; }

        public AllTimeHigh allTimeHigh { get; set; }
    }
}
