namespace SocialMediaPlatform.Services.Interfaces
{
	public interface IRecaptchaValidator
	{
		public Task<bool> ValidateRecaptcha(string recaptchaToken);
	}
}
