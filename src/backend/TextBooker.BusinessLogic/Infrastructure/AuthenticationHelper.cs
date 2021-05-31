using System;
using System.Text;
using System.Collections.Generic;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using TextBooker.Contracts.Dto.Config;

namespace TextBooker.BusinessLogic.Infrastructure
{
	public static class AuthenticationHelper
	{
		public static string GenerateJwtToken(string email, string userId, JwtSettings jwtSettings)
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, userId),
				new Claim(JwtRegisteredClaimNames.Sub, email),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var expires = DateTime.Now.AddDays(Convert.ToDouble(jwtSettings.ExpireDays));

			var token = new JwtSecurityToken(
				jwtSettings.Issuer,
				jwtSettings.Issuer,
				claims,
				expires: expires,
				signingCredentials: creds
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		public static bool ValidateJwtToken(string token, JwtSettings jwtSettings)
		{
			var validationParameters = new TokenValidationParameters()
			{
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidateLifetime = true,
				ValidateIssuerSigningKey = true,
				ValidIssuer = jwtSettings.Issuer,
				ValidAudience = jwtSettings.Issuer,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
				ClockSkew = TimeSpan.FromDays(30)
			};

			new JwtSecurityTokenHandler().ValidateToken(token, validationParameters, out var validToken);

			if (!( validToken is JwtSecurityToken validJwt ))
				return false;

			if (!validJwt.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.Ordinal))
				return false;

			return true;
		}
	}
}
