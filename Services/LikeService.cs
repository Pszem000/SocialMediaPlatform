using Microsoft.Azure.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SocialMediaPlatform.Models;
using SocialMediaPlatform.Services.Interfaces;

namespace SocialMediaPlatform.Services
{
	public class LikeService : ILikeService
	{
		
		private readonly AppDbContext _Context;
		private readonly IPostGetter _PostGetter;
		private readonly IUserGetter _UserGetter;
		public LikeService(AppDbContext Context, IPostGetter PostGetter, IUserGetter UserGetter)
		{
			_Context = Context;
			_PostGetter = PostGetter;
			_UserGetter = UserGetter;
		}
		public async Task AddLikeModel(string PostId,string UserId)
		{

			var Post = await _PostGetter.GetPostsById(PostId);
			if (!Post.Likes.Any(like => like.UserId == UserId))
			{
				var Likemodel = new LikeModel
				{
					UserId = UserId,
					PostId = PostId,
				};
				Post.Likes.Add(Likemodel);
				Post.NumberOfLikes++;
				await _Context.SaveChangesAsync();
			}
		}
		public async Task RemoveLikeModel(string PostId, string UserId)
		{
			var Post = await _PostGetter.GetPostsById(PostId);
			var User = await _UserGetter.GetUserById(UserId);
			if(Post.Likes.Where(like => like.UserId == UserId).Any())
			{
				var Likemodel = new LikeModel
				{
					UserId = UserId,
					PostId = PostId,
				};
				Post.Likes.Remove(Likemodel);
				Post.NumberOfLikes--;
				await _Context.SaveChangesAsync();

			}
		}			
	}
}
}
