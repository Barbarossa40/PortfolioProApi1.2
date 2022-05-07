using PortfolioProApi.Entities;

namespace PortfolioProApi.Models
{
    public class AuthResponseDto
    {
        public string Id { get; set; }
        public bool IsAuthenticated { get; set; }
        public string ErrorMessage { get; set; }
        public string Token { get; set; }

        public string Email { get; set; }
       
      //  public List<UserClaim> Claims { get; set; }
    }
}
