using SocialMediaPlatform.Models;

namespace SocialMediaPlatform.Services.Interfaces
{
	public interface IRegistserService
	{
		public Task<string> Register(RegisterModel UserData, IFormFile? ProfileImage, string recaptchaToken);
	}
}
