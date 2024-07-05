using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMediaPlatform.Models;
using SocialMediaPlatform.Services.Interfaces;

namespace SocialMediaPlatform.Services
{
	public class UsernameChanger : IUsernameChanger
	{
		private readonly IUserGetter _UserGetter;
		private readonly UserManager<UserModel> _UserManager;
		private readonly AppDbContext _Context;
		public UsernameChanger(IUserGetter UserGetter, AppDbContext Context, UserManager<UserModel> UserManager)
		{
			_UserGetter = UserGetter;
			_UserManager = UserManager;
			_Context = Context;
		}
		public async Task ChangeUsername(string CurrentPassword, string NewUsername, string UserId)
		{
			var User = await _UserGetter.GetUserById(UserId);
			if (User != null)
			{
				if (await _UserManager.CheckPasswordAsync(User, CurrentPassword))
				{
					var Result = await _UserManager.SetUserNameAsync(User, NewUsername);
					if (Result.Succeeded)
					{
						await _Context.SaveChangesAsync();
					}
				}
			}
		}
	}
}
