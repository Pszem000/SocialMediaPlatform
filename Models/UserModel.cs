using Microsoft.AspNetCore.Identity;
namespace SocialMediaPlatform.Models
{
	public class UserModel : IdentityUser
	{

		public int? ProfileImageId { get; set; }
		public bool IsOnline { get; set; }
		public string? RecoveryCode { get; set; }
	}
}
