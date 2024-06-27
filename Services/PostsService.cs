using Microsoft.EntityFrameworkCore;
using SocialMediaPlatform.Models;
using SocialMediaPlatform.Services.Interfaces;

namespace SocialMediaPlatform.Services
{
	public class PostsService : IPostsService
	{
		private readonly AppDbContext _Context;
		private readonly IPostGetter _PostGetter;
		public PostsService(AppDbContext Context, IPostGetter PostGetter)
		{
			_Context = Context;
			_PostGetter = PostGetter;
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
		public async Task EditPostContent(string NewContnet, string PostId)
		{
			var Post = await _PostGetter.GetPostsById(PostId);
			Post.Content = NewContnet;
			await _Context.SaveChangesAsync();
		}
	}
}
