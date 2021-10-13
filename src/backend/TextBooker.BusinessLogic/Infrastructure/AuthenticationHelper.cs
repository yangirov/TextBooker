using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using CSharpFunctionalExtensions;

using Microsoft.IdentityModel.Tokens;

using Newtonsoft.Json;

using TextBooker.Common.Config;
using TextBooker.Contracts.Dto;

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

		public static async Task<Result> RecaptchaTokenVerify(IHttpClientFactory clientFactory, GoogleSettings googleOptions, string token)
		{
			var tokenResponse = new TokenResponseModel() { Success = false };

			using (var client = clientFactory.CreateClient(HttpClientNames.GoogleRecaptcha))
			{
				var response = await client.GetStringAsync($"{googleOptions.RecaptchaVerifyApi}?secret={googleOptions.SecretKey}&response={token}");
				tokenResponse = JsonConvert.DeserializeObject<TokenResponseModel>(response);
			}

			return ( !tokenResponse.Success || tokenResponse.Score < (decimal)0.5 )
				 ? Result.Failure("Recaptcha token is invalid")
				 : Result.Success();
		}
	}
}
