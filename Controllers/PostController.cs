using Microsoft.AspNetCore.Mvc;
using SocialMediaPlatform.Services.Interfaces;

namespace SocialMediaPlatform.Controllers
{
	public class PostController : Controller
	{
		private readonly IPostsService _PostsService;
		public PostController(IPostsService PostsService)
		{
			_PostsService = PostsService;
		}
		[HttpPost]
		public async Task ChangeContent(string Content, string PostId)
		{
			await _PostsService.EditPostContent(Content, PostId);
		}
	}
}
