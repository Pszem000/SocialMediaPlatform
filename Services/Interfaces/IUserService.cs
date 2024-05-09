using Messenger.models;

namespace Messenger.Services.Interfaces
{
	public interface IUserService
	{
		public Task ChangePassword(UserModel User, string Password);
	}
}
