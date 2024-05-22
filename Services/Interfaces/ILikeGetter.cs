using SocialMediaPlatform.Models;

namespace SocialMediaPlatform.Services.Interfaces
{
	public interface ILikeGetter
	{   
		public Task<List<LikeModel>> GetLikesByPostId(string postId);
		public Task<LikeModel> GetCurrentLikeModel(string PostId, string UserId);
			
	}
}
