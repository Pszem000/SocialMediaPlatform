namespace SocialMediaPlatform.Services.Interfaces
{
	public interface IBioService
	{
		public Task ChangeBio(string Bio, string UserId);
	}
}
