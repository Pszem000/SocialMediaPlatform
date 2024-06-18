using Microsoft.AspNetCore.Mvc;
using SocialMediaPlatform.Services.Interfaces;

namespace SocialMediaPlatform.Controllers
{
	public class BioController : Controller
	{
		private readonly AppDbContext _Context;
		private readonly IUserGetter _UserGetter;
		public BioController(AppDbContext Context,IUserGetter UserGetter)
		{
			_Context = Context;
			_UserGetter = UserGetter;
		}
		[HttpPost]
		public async Task ChangeBio(string Bio,string UserId)
		{
			var User = await _UserGetter.GetUserById(UserId);
			User.Bio = Bio;
			await _Context.SaveChangesAsync();
		}
	}
}
