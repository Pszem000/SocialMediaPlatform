using Microsoft.EntityFrameworkCore;
using SocialMediaPlatform.Models;
using SocialMediaPlatform.Services.Interfaces;

namespace SocialMediaPlatform.Services
{
	public class PostsService : IPostsService
	{
		private readonly AppDbContext _Context;
		public PostsService(AppDbContext Context)
		{
			_Context = Context;
		}
		public async Task AddPost(PostModel Post)
		{
			await _Context.Posts.AddAsync(Post);
			await _Context.SaveChangesAsync();
		}
		public async Task RemovePost(string PostId)
		{
			var Post = await _Context.Posts.Where(x => x.Id == PostId).FirstOrDefaultAsync();
			_Context.Posts.Remove(Post);
			await _Context.SaveChangesAsync();
		}
	}
}
