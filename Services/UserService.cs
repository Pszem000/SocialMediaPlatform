using Messenger.models;
using Messenger.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Azure.Documents;
using NuGet.Common;

namespace Messenger.Services
{
	public class UserService : IUserService
	{
		private readonly AppDbContext _Context;
		private readonly IUserGetter _UserGetter;
		private readonly IRecoveryCodeGetter _RecoveryCodeGetter;
		private readonly UserManager<UserModel> _UserManager;
		public UserService(IUserGetter UserGetter, AppDbContext Context, IRecoveryCodeGetter RecoveryCodeGetter, UserManager<UserModel> UserManager)
		{
			_UserGetter = UserGetter;
			_Context = Context;
			_RecoveryCodeGetter = RecoveryCodeGetter;
			_UserManager = UserManager;
		}
		public async Task ChangePassword(UserModel User, string Password)
		{
			if (User != null)
			{
				var Token = await _UserManager.GeneratePasswordResetTokenAsync(User);
				await _UserManager.ResetPasswordAsync(User, Token, Password);
			}
		}

	}
}
