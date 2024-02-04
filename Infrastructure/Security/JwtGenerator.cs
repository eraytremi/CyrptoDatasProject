
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Utilities.Security.JWT
{
	public class JwtGenerator
	{
		private readonly IConfiguration _configuration;
		private TokenOptions _tokenOptions;
		private DateTime _expiration;

		public JwtGenerator(IConfiguration configuration)
		{
			_configuration = configuration;
			_tokenOptions = _configuration.GetSection("TokenOptions").Get<TokenOptions>();
		}

		public AccessToken CreateAccessToken(List<Claim> claims = null)
		{
			_expiration = DateTime.Now.AddDays(_tokenOptions.TokenExpiration);

			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey));
			var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			var jwt = CreateJwtSecurityToken(_tokenOptions, signingCredentials, claims);

			List<string> claimList = new List<string>();
			int i = 1;
			if (claims != null)
			{
				foreach (var claim in claims)
				{
					claimList.Add($"claim{i}");
					i++;
				}
			}

			var jwtHandler = new JwtSecurityTokenHandler();
			var token = jwtHandler.WriteToken(jwt);

			return new AccessToken
			{
				Token = token,
				Expiration = _expiration,
				Claims = new List<string> { "claim1", "claim2", "claim3" }
			};
		}


		private JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, SigningCredentials signingCredentials, List<Claim> claims = null)
		{
			if (claims == null)
			{
				claims = new List<Claim>()
				{
					new Claim("key1", "value1"),
					new Claim("key2", "value2"),
					new Claim("key2", "value2")
				};
			}


			var jwtSecurityToken = new JwtSecurityToken
			(
				issuer: tokenOptions.Issuer,
				audience: tokenOptions.Audience,
				expires: _expiration,
				notBefore: DateTime.Now,
				claims: claims,
				signingCredentials: signingCredentials
			);

			return jwtSecurityToken;
		}
	}
}
