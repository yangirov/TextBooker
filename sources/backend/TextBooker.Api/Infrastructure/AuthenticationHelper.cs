using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TextBooker.Api.Infrastructure.Models;
using TextBooker.Contracts.Dto;

namespace Api.Infrastructure
{
	public static class AuthenticationHelper
	{
		public static string GenerateJwtToken(string email, ApplicationUser user, JwtSettings jwtSettings)
		{
			var claims = new List<Claim>
			{
				new Claim(JwtRegisteredClaimNames.Sub, email),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable(jwtSettings.Key)));
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
	}
}
