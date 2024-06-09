namespace SocialMediaPlatform.Services.Interfaces
{
	public interface IFollowService
	{
		public Task AddFollow(string FollowerId,string FollowedId);
		public Task RemoveFollow(string FollowerId, string FollowedId);
	}
}
