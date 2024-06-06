using Microsoft.Azure.Documents;
using Microsoft.EntityFrameworkCore;
using SocialMediaPlatform.Models;
using SocialMediaPlatform.Services.Interfaces;
using System.Configuration;
namespace SocialMediaPlatform.Services
{
	public class ImageSaver : IImageSaver
	{
		private readonly AppDbContext _Context;
		private readonly IUserGetter _UserGetter;
		public ImageSaver(AppDbContext Context,IUserGetter userGetter)
		{
			_Context = Context;
			_UserGetter = userGetter;
		}

		public async Task SaveImage(IFormFile File, string UserId)
		{
			var FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ProfileImages", $"ProfileImage_{UserId}.jpg");

			using (var stream = new FileStream(FilePath, FileMode.Create))
			{
				await File.CopyToAsync(stream);
			}
			var User = await _UserGetter.GetUserById(UserId);
			User.ProfileImageSrc = FilePath;
			await _Context.SaveChangesAsync();
		}
	}
}
