using Messenger.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
namespace Messenger.models
{
	public class UserModel : IdentityUser
	{
		
		public int? ProfileImageId { get; set; }
		public bool IsOnline { get; set; }
		public string? RecoveryCode { get; set; }
	}
}
