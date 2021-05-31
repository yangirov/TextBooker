using System.Collections.Generic;
using Newtonsoft.Json;

namespace TextBooker.Contracts.Dto
{
	/// <summary>
	/// Response Model from Google Recaptcha V3 Verify API
	/// </summary>
	public class TokenResponseModel
	{
		[JsonProperty("success")]
		public bool Success { get; set; }

		[JsonProperty("score")]
		public decimal Score { get; set; }

		[JsonProperty("action")]
		public string Action { get; set; }

		[JsonProperty("error-codes")]
		public List<string> ErrorCodes { get; set; }
	}
}
