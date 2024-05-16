namespace SocialMediaPlatform.Services.Interfaces
{
	public interface ILikeService
	{
		public Task AddLikeModel(string PostId, string UserId);
	}
}
