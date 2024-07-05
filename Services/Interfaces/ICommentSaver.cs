namespace SocialMediaPlatform.Services.Interfaces
{
	public interface ICommentSaver
	{
		public Task AddComment(string Contnet, string PostId);
	}
}
