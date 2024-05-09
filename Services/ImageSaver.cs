using Messenger.Models;
using Messenger.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace Messenger.Services
{
	public class ImageSaver : IImageSaver
	{
		private readonly AppDbContext _Context;
		private MemoryStream MemoryStream = new MemoryStream();
		public ImageSaver(AppDbContext Context)
		{
			_Context = Context;
		}

		public async Task SaveImage(IFormFile File, string UserId)
		{
			File.CopyTo(MemoryStream);

			var Image = new ImageModel
			{
				image = MemoryStream.ToArray(),
				ContentType = File.ContentType,
				UserId = UserId

			};

			_Context.ImageList.Add(Image);
			_Context.SaveChanges();

			var User = await _Context.Users.Where(x => x.Id == UserId).FirstAsync();
			User.ProfileImageId = Image.Id;
			_Context.SaveChanges();
		}
	}
}
