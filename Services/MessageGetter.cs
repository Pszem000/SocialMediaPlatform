using Microsoft.EntityFrameworkCore;
using SocialMediaPlatform.Models;
using SocialMediaPlatform.Services.Interfaces;

namespace SocialMediaPlatform.Services
{
	public class MessageGetter : IMessageGetter
	{
		private readonly AppDbContext _Context;
		public MessageGetter(AppDbContext Context)
		{
			_Context = Context;
		}

		public async Task<List<MessageModel>> GetMessages(string CreatorId, string ReciverId)
		{
			var Messages = await _Context.MessageList
		   .Where(x => x.CreatorId == CreatorId && x.ReciverId == ReciverId || x.CreatorId == ReciverId && x.ReciverId == CreatorId)
		   .ToListAsync();

			return Messages;
		}
		public async Task<List<MessageModel>> GetMessagesByCreator(string CreatorId)
		{
			return await _Context.MessageList.Where(x => x.CreatorId == CreatorId).ToListAsync();
		}
		public async Task<List<MessageModel>> GetMessagesByReceiver(string ReceiverId)
		{
			return await _Context.MessageList.Where(x => x.ReciverId == ReceiverId).ToListAsync();
		}
	}
}
