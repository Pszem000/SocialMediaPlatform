using Microsoft.AspNetCore.Identity;
using Microsoft.Azure.Documents;
using SocialMediaPlatform;
using SocialMediaPlatform.Models;
using SocialMediaPlatform.Services.Interfaces;

namespace SocialMediaPlatform.Services
{
	public class RecoveryCodeGetter : IRecoveryCodeGetter
	{
		private readonly AppDbContext _Context;
		private readonly IUserGetter _UserGetter;
		private readonly UserManager<UserModel> _UserManager;

		public RecoveryCodeGetter(AppDbContext Context, IUserGetter UserGetter, UserManager<UserModel> UserManager)
		{
			_Context = Context;
			_UserGetter = UserGetter;
			_UserManager = UserManager;
		}
		public string GetRecoveryCode(string UserId)
		{
			var RecoveryCode = _Context.Users.Where(user => user.Id == UserId).FirstOrDefault().RecoveryCode;
			return RecoveryCode;
		}

		public async Task<string> GenerateRecoveryCode(string UserId)
		{
			var User = await _UserGetter.GetUserById(UserId);
			if (User != null)
			{
				var token = await _UserManager.GeneratePasswordResetTokenAsync(User);
				return token;
			}
			return null;

		}
	}
}
