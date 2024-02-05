using Business.Abstract;
using Infrastructure.Utilities.Security.JWT;
using Microsoft.AspNetCore.Mvc;
using Model.Dtos.UserDto;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController:BaseController
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Login ([FromBody]LoginDto dto)
        {
            var response = _userService.Login(dto.Email, dto.Password);
            var claims = new List<Claim>() {
                new Claim(JwtRegisteredClaimNames.NameId, response.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var accessToken = new JwtGenerator(_configuration).CreateAccessToken(claims);
            response.Token = accessToken.Token;
            return Ok(response);
        }

        [HttpGet("varliklarim")]
        public async Task<IActionResult> GetVarliklarim()
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _userService.GetUserVarliklar(currentUserId.GetValueOrDefault());
            return Ok(response);
        }


        [HttpGet("getmytrades")]
        public async Task<IActionResult> GetMyTrades()
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _userService.GetMyTrades(currentUserId.GetValueOrDefault());
            return Ok(response);
        }
    }
}
