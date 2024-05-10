using Microsoft.EntityFrameworkCore;
using SocialMediaPlatform;
using SocialMediaPlatform.Models;
using SocialMediaPlatform.Services.Interfaces;

namespace SocialMediaPlatform.Services
{
	public class MessageService : IMessageService
	{
		private readonly AppDbContext _Context;
		public MessageService(AppDbContext Context)
		{
			_Context = Context;
		}

		public async Task<string> AddMessage(string MessageContent, string ReceiverId, string CreatorId)
		{
			var Message = new MessageModel
			{
				Content = MessageContent,
				CreatorId = CreatorId,
				ReciverId = ReceiverId
			};

			_Context.MessageList.Add(Message);
			await _Context.SaveChangesAsync();
			return Message.Id.ToString();
		}

	}
}
