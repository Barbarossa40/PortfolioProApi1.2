using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PortfolioProApi.Models;


namespace PortfolioProApi.Entities
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Time Of Transaction")]
        public DateTime TransactionTime { get; set; }

        public double TransactionAmount { get; set; }

        public double TotalAmount { get; set; }

        public string UserId { get; set; }
               
        [ForeignKey("AssetId")]
        public int AssetId { get; set; }
        public virtual Asset? Asset { get; set; }

        public double PriceSnapshot { get; set; }

    }
}
