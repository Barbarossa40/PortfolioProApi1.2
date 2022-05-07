using PortfolioProApi.Entities;

namespace PortfolioProApi.Models
{
    public class TransactionDto
    {
        public  DateTime transactionTime { get; set; }

        public int transactionAmount { get; set; }

        public int totalAmount { get; set; }

        public string UserId { get; set; }

        public int AssetId { get; set; }

        public double PriceSnapshot { get; set; } 

    }
}
