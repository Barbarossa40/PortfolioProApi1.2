using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using PortfolioProApi.Models;


namespace PortfolioProApi.Entities
{
    public class User : IdentityUser
    {   
       
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<Asset> Assets { get; set; }

    }
}


