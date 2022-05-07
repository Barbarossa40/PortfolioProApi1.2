using PortfolioProApi.Entities;

namespace PortfolioProApi.Models
{
    public class TransactionPutDto
    {
        public int TransactionId { get; set; }

        public DateTime TransactionTime { get; set; }

        public double TransactionAmount { get; set; }

        public double TotalAmount { get; set; }

        public string UserId { get; set; }

        public int AssetId { get; set; }
      
    }
}
