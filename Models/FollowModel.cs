using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMediaPlatform.Models
{
	public class FollowModel
	{
		[Key]
		public string Id { get; set; } = Guid.NewGuid().ToString();
		[ForeignKey(nameof(Follower))]
		public string FollowerId { get; set; }
		public UserModel Follower { get; set; }
		[ForeignKey(nameof(Followed))]
		public string FollowedId { get; set; }
		public UserModel Followed { get; set; }
	}
}
