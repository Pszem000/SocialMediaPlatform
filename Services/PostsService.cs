using Microsoft.EntityFrameworkCore;
using SocialMediaPlatform.Models;
using SocialMediaPlatform.Services.Interfaces;

namespace SocialMediaPlatform.Services
{
	public class PostsService : IPostsService
	{
		private readonly AppDbContext _Context;
		private readonly IPostGetter _PostGetter;
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
			var Post = await _PostGetter.GetPostsById(PostId);
			_Context.Posts.Remove(Post);
			await _Context.SaveChangesAsync();
		}
		public async Task EditPostContent(string PostId, string NewContnet)
		{
			var Post = await _PostGetter.GetPostsById(PostId);
			Post.Content = NewContnet;
			await _Context.SaveChangesAsync();
		}
	}
}
