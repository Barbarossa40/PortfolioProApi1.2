using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PortfolioProApi.Models;



namespace PortfolioProApi.Entities
{
    public class PortfolioProApiContext : IdentityDbContext<User>
    {
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _webApiOptions;
        private readonly string _connectionString;
        public PortfolioProApiContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _webApiOptions = _configuration.GetSection("WebApiOptions");
            _connectionString = _webApiOptions.GetSection("ConnectionString").Value;
        }


        public DbSet<Asset> Assets { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer(_connectionString);


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Asset>().HasData(
                new Asset
                {
                    AssetId = 1,
                    AssetName = "Microsoft",
                    StockSymbol = "MSFT",
                    Type = "Stock"

                },
                new Asset
                {
                    AssetId = 2,
                    AssetName = "Apple",
                    StockSymbol = "AAPL",
                    Type = "Stock"
                },
                new Asset
                {
                    AssetId = 3,
                    AssetName = "Bitcoin",
                    StockSymbol = "BTC",
                    Type = "Crypto"
                }
            );

        }
    }
}
