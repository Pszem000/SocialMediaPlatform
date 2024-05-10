namespace SocialMediaPlatform.Services.Interfaces
{
	public interface IImageSaver
	{
		public Task SaveImage(IFormFile File, string UserId);
	}
}
