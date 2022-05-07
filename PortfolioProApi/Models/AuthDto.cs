using System.ComponentModel.DataAnnotations;
using PortfolioProApi.Entities;

namespace PortfolioProApi.Models
{
    public class AuthDto
    {

        [Required(ErrorMessage = "Email Required, Yo!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password Required, My Man!.")]
        public string Password { get; set; }
    }
}
