using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMediaPlatform;

namespace SocialMediaPlatform.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class MessengerController : Controller
	{
		private readonly AppDbContext _Context;

		public MessengerController(AppDbContext context)
		{
			_Context = context;
		}

		[HttpGet]
		[Route("/DisplayImage/{imageId}")]
		public IActionResult DisplayImage(int ImageId)
		{
			var Image = _Context.ImageList.FirstOrDefault(i => i.Id == ImageId);

			if (Image == null)
			{
				return NotFound();
			}

			return File(Image.image, Image.ContentType);
		}
		[HttpGet]
		[Route("/DeleteMessage/{MessageId}")]
		public async Task DeleteMessage(int MessageId)
		{
			var Message = await _Context.MessageList.Where(msg => msg.Id == MessageId).FirstAsync();
			if (Message != null)
			{
				_Context.MessageList.Remove(Message);
				await _Context.SaveChangesAsync();
			}
		}
	}
}
