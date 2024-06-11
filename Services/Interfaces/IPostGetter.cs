using SocialMediaPlatform.Models;

namespace SocialMediaPlatform.Services.Interfaces
{
	public interface IPostGetter
	{
		public Task<List<PostModel>> GetPosts();
		public Task<PostModel> GetPostsById(string PostId);
		public Task<List<PostModel>> GetPostsByCreatorId(string CreatorId);
	}
}
