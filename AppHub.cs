using Microsoft.AspNetCore.SignalR;
namespace SocialMediaPlatform
{
	public class AppHub : Hub
	{
		public static Dictionary<string, string> ConnectionUserMap = new Dictionary<string, string>();

		public override async Task OnDisconnectedAsync(Exception Exception)
		{
			var connectionId = Context.ConnectionId;
			ConnectionUserMap.Remove(connectionId);
			await base.OnDisconnectedAsync(Exception);
		}
		public override async Task OnConnectedAsync()
		{
			var UserId = Context.GetHttpContext().Request.Headers["UserId"].ToString();
			var ConnectionId = Context.ConnectionId;
			ConnectionUserMap[ConnectionId] = UserId;
			await base.OnConnectedAsync();
		}
		public Dictionary<string, string> GetConnectionUserMap()
		{
			return ConnectionUserMap;
		}
		public string GetConnectionId()
		{
			return Context.ConnectionId.ToString();
		}
		public async Task SendMessage(string Message, string UserId, string MessageId)
		{

			await Clients.Groups(UserId).SendAsync("ReciveNotification", Message, MessageId);

		}
		public async Task JoinGroup(string UserId)
		{
			await Groups.AddToGroupAsync(Context.ConnectionId, UserId);

		}
		public async Task ReadMessages(string UserId)
		{
			await Clients.Groups(UserId).SendAsync("ReadMessages");
		}
		public async Task ChangeMessageNotification(string UserId)
		{
			await Clients.Groups(UserId).SendAsync("ChangeMessageNotification");
		}
	}
}