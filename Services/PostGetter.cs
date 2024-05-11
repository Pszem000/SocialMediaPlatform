using Microsoft.EntityFrameworkCore;
using SocialMediaPlatform.Models;
using SocialMediaPlatform.Services.Interfaces;

namespace SocialMediaPlatform.Services
{
	public class PostGetter : IPostGetter
	{
		private readonly AppDbContext _Context;
		public PostGetter(AppDbContext Context) 
		{
			_Context = Context;
		}
		public async Task<List<PostModel>> GetPosts()
		{
			var Posts = await _Context.Posts.OrderByDescending(x => x.PublicationDate).ToListAsync();
			return Posts;
		}
	}
}
