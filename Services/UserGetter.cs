using Microsoft.AspNetCore.Identity;
using SocialMediaPlatform.Models;
using SocialMediaPlatform.Services.Interfaces;

namespace SocialMediaPlatform.Services
{
	public class UserGetter : IUserGetter
	{

		private readonly UserManager<UserModel> _UserManager;
		private readonly IHttpContextAccessor _HttpContextAccessor;
		private readonly IMessageGetter _MessageGetter;
		public UserGetter(IMessageGetter MessageGetter, UserManager<UserModel> UserManager, IHttpContextAccessor httpContextAccessor)
		{
			_UserManager = UserManager;
			_HttpContextAccessor = httpContextAccessor;
			_MessageGetter = MessageGetter;
		}
		public async Task<UserModel> GetLoggedUser()
		{
			var User = await _UserManager.GetUserAsync(_HttpContextAccessor.HttpContext.User);
			if (User != null)
			{
				return User;
			}
			return null;
		}
		public async Task<UserModel> GetUserById(string Id)
		{
			var User = await _UserManager.FindByIdAsync(Id);
			return User;
		}
		public async Task<List<UserModel>> GetUsers(string CreatorId)
		{
			var MessageList = await _MessageGetter.GetMessagesByCreator(CreatorId);

			MessageList = MessageList.Concat(await _MessageGetter.GetMessagesByReceiver(CreatorId)).ToList();

			var GroupedMessageList = MessageList.GroupBy(msg => msg.CreatorId);

			GroupedMessageList = GroupedMessageList.Concat(MessageList.GroupBy(msg => msg.ReciverId));

			var OrderedGroupedMessageList = GroupedMessageList.OrderBy(group => group.Max(msg => msg.SendTime));

			var Ids = OrderedGroupedMessageList.Select(group => group.Key).ToList();
			Ids.Reverse();
			var UserList = new List<UserModel>();

			foreach (var Id in Ids)
			{
				if (!(Id == CreatorId) && !UserList.Contains(await GetUserById(Id)))
				{
					UserList.Add(await GetUserById(Id));
				}
			}
			return UserList;
		}
		public async Task<UserModel> GetUserByEmail(string Email)
		{
			var User = await _UserManager.FindByEmailAsync(Email);
			return User;
		}
	}
}
