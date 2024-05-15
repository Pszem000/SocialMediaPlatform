using Microsoft.EntityFrameworkCore;
using SocialMediaPlatform.Models;
using SocialMediaPlatform.Services.Interfaces;

namespace SocialMediaPlatform.Services
{
	public class LikeGetter : ILikeGetter
	{
		private readonly AppDbContext _Context;
		public LikeGetter(AppDbContext context)
		{
			_Context = context;
		}
		public async Task<List<LikeModel>> GetLikesByPostId(string postId)
		{
			var Likes = await _Context.LikeList.Where(x =>  x.PostId == postId).ToListAsync();
			return Likes;
		}
	}
}
