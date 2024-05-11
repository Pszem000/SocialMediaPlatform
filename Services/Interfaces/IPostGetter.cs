using SocialMediaPlatform.Models;

namespace SocialMediaPlatform.Services.Interfaces
{
	public interface IPostGetter
	{
		public Task<List<PostModel>> GetPosts();
	}
}
