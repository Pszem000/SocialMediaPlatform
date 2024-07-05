using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMediaPlatform;
using SocialMediaPlatform.Models;
using SocialMediaPlatform.Services.Interfaces;

namespace SocialMediaPlatform.Controllers
{
	public class UsernameController : Controller
	{
		private readonly IUsernameChanger _UsernameChanger;
		public UsernameController(IUsernameChanger usernameChanger)
		{
			_UsernameChanger = usernameChanger;
		}

		[HttpPost]
		public async Task<IActionResult> ChangeUsername(string CurrentPassword, string NewUsername, string UserId)
		{
			await _UsernameChanger.ChangeUsername(CurrentPassword, NewUsername, UserId);
			return Redirect("/Account");
		}
	}
}
