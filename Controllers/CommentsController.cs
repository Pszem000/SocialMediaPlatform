using Microsoft.AspNetCore.Mvc;
using SocialMediaPlatform.Models;
using SocialMediaPlatform.Services.Interfaces;

namespace SocialMediaPlatform.Controllers
{
	public class CommentsController : Controller
	{
		private readonly AppDbContext _Context;
		private readonly IUserGetter _UserGetter;
		public CommentsController(AppDbContext Context,IUserGetter UserGetter)
		{
			_Context = Context;
			_UserGetter = UserGetter;
		}
		public async Task AddComment(string Content,string PostId)
		{
			var User = await _UserGetter.GetLoggedUser();
			if (User != null)
			{
				var Comment = new CommentModel
				{
					Content = Content,
					PostId = PostId,
					CreatorId = User.Id
				};
				await _Context.Comments.AddAsync(Comment);
				await _Context.SaveChangesAsync();
			}
		}
	}
}
