  using System.IdentityModel.Tokens.Jwt;
using PortfolioProApi.Models;
using PortfolioProApi.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using PortfolioProApi.JwtServices;

namespace PortfolioProApi.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly JwtHandler _jwtHandler;
       

        public AccountsController(UserManager<User> userManager, IMapper mapper, JwtHandler jwtHandler)
        {
            _userManager = userManager;
            _mapper = mapper;
            _jwtHandler = jwtHandler;
       
        }

        [HttpPost("Registration")]
        public async Task<IActionResult> RegisterUser([FromBody] RegistrationDto userReg)
        {
            if (userReg == null || !ModelState.IsValid)
                return BadRequest();

            var user = _mapper.Map<User>(userReg);

            var result = await _userManager.CreateAsync(user, userReg.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);

                return BadRequest(new RegResponseDto { Errors = errors });
            }


            return Ok(new RegResponseDto { RegSuccessful= true});
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] AuthDto userLogin)
        {
            var user = await _userManager.FindByNameAsync(userLogin.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, userLogin.Password))
                return Unauthorized(new AuthResponseDto { ErrorMessage = "Invalid Authentication" });

            var signingCredentials = _jwtHandler.GetSigningCredentials();
            var claims = await _jwtHandler.GetClaims(user);
            var tokenOptions = _jwtHandler.GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return Ok(new AuthResponseDto {Id = user.Id, IsAuthenticated = true, Token = token, Email = user.Email});

        }
    
        
    }
}