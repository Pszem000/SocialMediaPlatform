using Messenger.Models;

namespace Messenger.Services.Interfaces
{
	public interface IImageSaver
	{
		public Task SaveImage(IFormFile File, string UserId);
	}
}
