using SocialMediaPlatform.Models;

namespace SocialMediaPlatform.Services.Interfaces
{
	public interface ILoginService
	{
		public Task<string> Login(LoginModel UserData, string recaptchaToken);
	}
}
