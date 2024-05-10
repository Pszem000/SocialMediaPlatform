using SocialMediaPlatform.Models;

namespace SocialMediaPlatform.Services.Interfaces
{
	public interface IUserGetter
	{
		public Task<UserModel> GetLoggedUser();
		public Task<UserModel> GetUserById(string Id);
		public Task<UserModel> GetUserByEmail(string Email);
		public Task<List<UserModel>> GetUsers(string CreatorId);

	}
}
