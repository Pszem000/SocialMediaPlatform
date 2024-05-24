using Microsoft.AspNet.SignalR.Hosting;
using Microsoft.AspNetCore.Mvc;
using SocialMediaPlatform.Models;
using SocialMediaPlatform.Services.Interfaces;

namespace SocialMediaPlatform.Controllers
{
	public class MainPageController : Controller
	{
		private readonly IPostsService _PostService;
		private readonly IPostGetter _PostGetter;
		private readonly IUserGetter _UserGetter;
		private UserModel User;
		public MainPageController(IPostsService postService, IUserGetter userGetter, IPostGetter postGetter)
		{ 
			_PostService = postService;
			_UserGetter = userGetter;
			_PostGetter = postGetter;
			User = _UserGetter.GetLoggedUser().Result;
		}
		public IActionResult Index()
		{
			return View();
		}
		[Route("MainPage")]
		public async Task<IActionResult> MainPage()
		{
			var Posts = await _PostGetter.GetPosts();
			foreach (var Post in Posts) 
			{
				if(Post.Likes.Where(x => x.UserId == User.Id).Count() != 0)
				{
					
					Post.IsLikedByCurrentUser = true;
				}	
			}
			return View(Posts);
		}
		[HttpGet]
		public async Task<IActionResult> SavePost(string Content)
		{
			//nie strzela w breakpoint nie wiem czemu
			if(User != null)
			{
				var postModel = new PostModel 
				{
					Content = Content,
					CreatorId = User.Id
				};
				await _PostService.AddPost(postModel);
			}
			return Ok();
		}
	}
}
