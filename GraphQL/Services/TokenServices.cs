using Domain;
using GraphQL.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace GraphQL.Services
{
	public class TokenServices
	{
		private readonly IOptions<TokenSettings> _tokenSettings;

		public TokenServices(IOptions<TokenSettings> tokenSettings)
		{
			_tokenSettings = tokenSettings;
		}

		public string CreateToken(User user, List<string> roles)
		{

			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, user.UserName),
				new Claim(ClaimTypes.NameIdentifier, user.Id),
				new Claim(ClaimTypes.Email, user.Email)
			};

			foreach (var role in roles)
			{
				claims.Add(new Claim(ClaimTypes.Role, role));
			}

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.Value.Key));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

			var securityDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.UtcNow.AddMinutes(20),
				SigningCredentials = creds,
				Issuer = _tokenSettings.Value.Issuer,
				Audience = _tokenSettings.Value.Audience
			};

			var tokenHandler = new JwtSecurityTokenHandler();

			var token = tokenHandler.CreateToken(securityDescriptor);

			return tokenHandler.WriteToken(token);
		}

		public RefreshToken GetRefreshToken()
		{
			var randomNumber = new byte[32];
			using var rng = RandomNumberGenerator.Create();
			rng.GetBytes(randomNumber);
			return new RefreshToken { Token = Convert.ToBase64String(randomNumber) };
		}
	}
}
