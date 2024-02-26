using Business.Abstract;
using Business.Exceptions;
using Infrastructure.Utilities.Security.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
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
                new Claim(JwtRegisteredClaimNames.NameId, response.Data.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var accessToken = new JwtGenerator(_configuration).CreateAccessToken(claims);
            response.Data.Token = accessToken.Token;
            return SendResponse(response);
        }

        [HttpGet("varliklarim")]
        public async Task<IActionResult> GetVarliklarim()
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _userService.GetUserVarliklar(currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }

        [HttpGet("getmytrades")]
        public async Task<IActionResult> GetMyTrades()
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            if (currentUserId==null)
            {
                throw new BadRequestException("kullanıcı girişi yok");
            }
            var response = await _userService.GetMyTrades(currentUserId.GetValueOrDefault());
            return Ok(response);
        }
    }
}
