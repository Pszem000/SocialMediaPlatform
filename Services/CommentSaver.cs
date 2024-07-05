using Microsoft.EntityFrameworkCore;
using SocialMediaPlatform.Models;
using SocialMediaPlatform.Services.Interfaces;

namespace SocialMediaPlatform.Services
{
	public class CommentSaver : ICommentSaver
	{
		private readonly IUserGetter _UserGetter;
		private readonly IPostGetter _PostGetter;
		private readonly AppDbContext _Context;
		public CommentSaver(IUserGetter userGetter, IPostGetter postGetter, AppDbContext context)
		{
			_UserGetter = userGetter;
			_PostGetter = postGetter;
			_Context = context;
		}
		public async Task AddComment(string Content, string PostId)
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
