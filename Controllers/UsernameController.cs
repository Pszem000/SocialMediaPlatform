using Messenger.models;
using Messenger.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Controllers
{
	public class UsernameController : Controller
	{
		private readonly IUserGetter _UserGetter;
		private readonly UserManager<UserModel> _UserManager;
		private readonly AppDbContext _Context;
		public UsernameController(IUserGetter UserGetter, AppDbContext Context, UserManager<UserModel> UserManager)
		{
			_UserGetter = UserGetter;
			_UserManager = UserManager;
			_Context = Context;
		}

		[HttpPost]
		public async Task<IActionResult> ChangeUsername(string CurrentPassword, string NewUsername, string UserId)
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
			return Redirect("/");
		}
	}
}
