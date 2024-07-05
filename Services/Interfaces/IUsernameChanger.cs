namespace SocialMediaPlatform.Services.Interfaces
{
	public interface IUsernameChanger
	{
		public Task ChangeUsername(string CurrentPassword, string NewUsername, string UserId);
	}
}
