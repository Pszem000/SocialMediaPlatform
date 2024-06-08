using Microsoft.AspNetCore.Identity;
namespace SocialMediaPlatform.Models
{
	public class UserModel : IdentityUser
	{
		public bool IsOnline { get; set; }
		public string? RecoveryCode { get; set; }
		public List<LikeModel> Likes { get; set; }
		public string? ProfileImageSrc { get; set; }
		public int NumberOfFollowers { get; set; }
		public int NumberOfFollowing { get; set;}
		public List<FollowModel> Followers { get; set;}
		public List<FollowModel> FollowedUsers {  get; set; }
		public string Bio { get; set; }
	}
}
