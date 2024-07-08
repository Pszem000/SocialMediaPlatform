using Newtonsoft.Json;
using reCAPTCHA.AspNetCore;
using SocialMediaPlatform.Services.Interfaces;

namespace SocialMediaPlatform.Services
{
	public class RecaptchaValidator : IRecaptchaValidator
	{
		private readonly string SecretKey;
		public RecaptchaValidator(IConfiguration Configuration)
		{
			SecretKey = Configuration.GetValue<string>("AppSettings:ReCAPTCHA_SecretKey");
		}
		public async Task<bool> ValidateRecaptcha(string recaptchaToken)
		{
			var httpClient = new HttpClient();
			var response = await httpClient.PostAsync($"https://www.google.com/recaptcha/api/siteverify?secret={SecretKey}&response={recaptchaToken}", null);
			var responseString = await response.Content.ReadAsStringAsync();
			var recaptchaResponse = JsonConvert.DeserializeObject<RecaptchaResponse>(responseString);
			return recaptchaResponse.success;
		}
	}
}
