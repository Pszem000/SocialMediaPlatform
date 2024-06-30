using Microsoft.AspNetCore.Mvc;
using SocialMediaPlatform.Services.Interfaces;

namespace SocialMediaPlatform.Controllers
{
	public class BioController : Controller
	{
		private readonly IBioService _BioService;
		public BioController(IBioService BioService)
		{
			_BioService = BioService;
		}
		[HttpPost]
		public async Task ChangeBio(string Bio, string UserId)
		{
			await _BioService.ChangeBio(Bio, UserId);
		}
	}
}
