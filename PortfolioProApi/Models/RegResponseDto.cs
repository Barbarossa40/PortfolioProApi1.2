using System.ComponentModel.DataAnnotations;
using PortfolioProApi.Entities;

namespace PortfolioProApi.Models
{
    public class RegResponseDto
    {
        public bool RegSuccessful { get; set; }
        public IEnumerable<string> Errors { get; set; }
       
    }
}
