using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PortfolioProApi.Models;


namespace PortfolioProApi.Entities
{
    public class Asset
    {
        [Key]
        public int AssetId { get; set; }

        
        [Display(Name = "Asset Name")]
        public string AssetName { get; set; }

        [StringLength(10)]
        public string StockSymbol { get; set; }

        [StringLength(20)]
        public string? Type { get; set; }

        public string? Uuid { get; set; }

       

    }
}
