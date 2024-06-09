using SocialMediaPlatform.Models;

namespace SocialMediaPlatform.Services.Interfaces
{
	public interface IFollowGetter
	{
		public Task<FollowModel> GetCurrentFollow(string FollowerId,string FollowedId);
		public Task<List<FollowModel>> GetFollowersCurrentUser(string UserId);
		public Task<List<FollowModel>> GetFollowsCurrentUser(string UserId);
	}
}
