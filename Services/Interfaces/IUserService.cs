using SocialMediaPlatform.Models;

namespace SocialMediaPlatform.Services.Interfaces
{
	public interface IUserService
	{
		public Task ChangePassword(UserModel User, string Password);
	}
}
