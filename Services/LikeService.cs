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
		private readonly ILikeGetter _LikeGetter;
		public LikeService(AppDbContext Context, IPostGetter PostGetter, IUserGetter UserGetter, ILikeGetter LikeGetter)
		{
			_Context = Context;
			_PostGetter = PostGetter;
			_UserGetter = UserGetter;
			_LikeGetter = LikeGetter;
		}
		public async Task AddLikeModel(string PostId,string UserId)
		{
			var Post = await _PostGetter.GetPostsById(PostId);
			if (await _LikeGetter.GetCurrentLikeModel(PostId, UserId) == null)
			{
				var Likemodel = new LikeModel
				{
					UserId = UserId,
					PostId = PostId,
				};
				
				Post.NumberOfLikes++;
				_Context.LikeList.Add(Likemodel);
				await _Context.SaveChangesAsync();
			}
		}
		public async Task RemoveLikeModel(string PostId, string UserId)
		{
			var Post = await _PostGetter.GetPostsById(PostId);
			var User = await _UserGetter.GetUserById(UserId);
			if (await _LikeGetter.GetCurrentLikeModel(PostId,UserId) != null)
			{
				var Likemodel = new LikeModel
				{
					UserId = UserId,
					PostId = PostId,
				};
				
				Post.NumberOfLikes--;
				var Like = await _Context.LikeList.Where(x => x.UserId == UserId && x.PostId == PostId).FirstOrDefaultAsync();
				_Context.LikeList.Remove(Like);
				await _Context.SaveChangesAsync();

			}
		}			
	}
}


