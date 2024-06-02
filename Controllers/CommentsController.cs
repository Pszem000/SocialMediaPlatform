using Microsoft.AspNetCore.Mvc;
using SocialMediaPlatform.Models;
using SocialMediaPlatform.Services.Interfaces;

namespace SocialMediaPlatform.Controllers
{
	public class CommentsController : Controller
	{
		private readonly AppDbContext _Context;
		private readonly IUserGetter _UserGetter;
		private readonly IPostGetter _PostGetter;
		public CommentsController(AppDbContext Context,IUserGetter UserGetter,IPostGetter PostGetter)
		{
			_Context = Context;
			_UserGetter = UserGetter;
			_PostGetter = PostGetter;
		}
		public async Task AddComment(string Content,string PostId)
		{
			var User = await _UserGetter.GetLoggedUser();
			
			if (User != null)
			{
				var Post = await _PostGetter.GetPostsById(PostId);
				var Comment = new CommentModel
				{
					Content = Content,
					PostId = PostId,
					CreatorId = User.Id
				};
				Post.NumberOfComments++;
				await _Context.Comments.AddAsync(Comment);
				await _Context.SaveChangesAsync();
			}
		}
	}
}
