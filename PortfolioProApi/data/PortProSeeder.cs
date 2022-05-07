using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using PortfolioProApi.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using PortfolioProApi.Entities;



namespace PortfolioProApi.data
{
    public class PortProSeeder
    {

        private readonly PortfolioProApiContext _ctx;
        private readonly UserManager<User> _userManager;

        public PortProSeeder(PortfolioProApiContext ctx, UserManager<User> userManager)
        {
            _ctx = ctx;
            _userManager = userManager;
        }
        public async Task SeedAsync()
        {
            _ctx.Database.EnsureCreated();

            User user = await _userManager.FindByEmailAsync("geoffreyorama@gmail.com");
            if (user == null)
            {
                user = new User()
                {
                    FirstName = "Geoffrey",
                    LastName = "Charles",
                    UserName = "geoffreyorama@gmail.com",
                    Email = "geoffreyorama@gmail.com"
                };
                var result = await _userManager.CreateAsync(user, "PhillCat@1154!");

                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create new user in Seeder");
                }
                
            }
            if (!_ctx.Assets.Any())
            {
                Asset asset = new Asset()
                {
                    AssetId = 1,
                    AssetName = "Microsoft",
                    StockSymbol = "MSFT",
                    Type = "Stock"
                };
                Asset assetTwo = new Asset()
                {
                    AssetId = 2,
                    AssetName = "Apple Inc.",
                    StockSymbol = "AAPL",
                    Type = "Stock"
                };

                Asset assetThree = new Asset()
                {
                    AssetId = 3,
                    AssetName = "Bitcoin",
                    StockSymbol = "BTC",
                    Type = "Cryptocurrency"
                };

                _ctx.Assets.AddRange(asset,assetTwo, assetThree);

                if (!_ctx.Transactions.Any())
                {
                    Transaction transaction = new Transaction()
                    {
                        TransactionId = 1,
                        TransactionTime = DateTime.Now,
                        TransactionAmount = 5,
                        TotalAmount = 10,
                        UserId = user.Id,
                        Asset = assetTwo,
                        //AssetId=assetTwo.AssetId
                    };
                }
                _ctx.SaveChanges();

            }

        }
        
    }
}




