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
			var Posts = await _Context.Posts.Include(x=> x.Creator).Include(x => x.Likes).Include(x => x.Comments).OrderByDescending(x => x.PublicationDate).ToListAsync();
			return Posts;
		}
		public async Task<PostModel> GetPostsById(string PostId)
		{			
			var Post = await _Context.Posts.Where(x => x.Id == PostId).Include(x => x.Creator).Include(x => x.Likes).Include(x => x.Comments).FirstOrDefaultAsync();
			return Post;
		}
		public async Task<List<PostModel>> GetPostsByCreatorId(string CreatorId)
		{
			var Posts = await _Context.Posts.Where(x => x.CreatorId == CreatorId).Include(x => x.Creator).Include(x => x.Likes).Include(x => x.Comments).ToListAsync();
			return Posts;
		}
	}
}
