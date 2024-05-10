namespace SocialMediaPlatform.Services.Interfaces
{
	public interface IMessageService
	{
		public Task<string> AddMessage(string MessageContent, string ReciverId, string CreatorId);

	}
}
