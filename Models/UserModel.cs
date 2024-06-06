using Microsoft.AspNetCore.Identity;
namespace SocialMediaPlatform.Models
{
	public class UserModel : IdentityUser
	{
		public bool IsOnline { get; set; }
		public string? RecoveryCode { get; set; }
		public ICollection<LikeModel> Likes { get; set; }
		public string? ProfileImageSrc { get; set; }
	}
}
