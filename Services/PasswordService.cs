using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMediaPlatform.Models;
using SocialMediaPlatform.Services.Interfaces;

namespace SocialMediaPlatform.Services
{
	public class PasswordService : IPasswordService
	{
		private readonly IUserGetter _UserGetter;
		private readonly UserManager<UserModel> _UserManager;
		private readonly AppDbContext _Context;
		private readonly IUserService _UserService;
		private readonly IRecoveryCodeGetter _RecoveryCodeGetter;
		public PasswordService(IUserGetter UserGetter, UserManager<UserModel> UserManager, AppDbContext Context, IUserService UserService, IRecoveryCodeGetter RecoveryCodeGetter)
		{
			_UserGetter = UserGetter;
			_UserManager = UserManager;
			_Context = Context;
			_UserService = UserService;
			_RecoveryCodeGetter = RecoveryCodeGetter;
		}
		public async Task<string> ChangePasswordByPassword(string CurrentPassword, string NewPassword, string UserId)
		{
			var User = await _UserGetter.GetUserById(UserId);
			if (User != null)
			{
				if (await _UserManager.CheckPasswordAsync(User, CurrentPassword))
				{
					var Result = await _UserManager.ChangePasswordAsync(User, CurrentPassword, NewPassword);
					if (Result.Succeeded)
					{
						await _Context.SaveChangesAsync();
					}
				}
			}
			return null;
		}
		public async Task<string> ChangePasswordByRecoveryCode(ChangePasswordModel ChangePasswordModel)
		{
			if (ChangePasswordModel.NewPassword == ChangePasswordModel.ConfirmPassword)
			{
				var User = await _UserGetter.GetUserByEmail(ChangePasswordModel.Email);

				var UserRecoveryCode = _RecoveryCodeGetter.GetRecoveryCode(User.Id);

				if (UserRecoveryCode == ChangePasswordModel.RecoveryCode)
				{
					await _UserService.ChangePassword(User, ChangePasswordModel.NewPassword);
				}
				else
				{
					return "Recovery code is wrong";
				}
			}
			else
			{
				return "Passwords are not the same";

			}
			return null;
		}
	}
}
