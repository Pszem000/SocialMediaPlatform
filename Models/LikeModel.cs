using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMediaPlatform.Models
{
	public class LikeModel
	{
		[Key]
		public string Id { get; set; } = Guid.NewGuid().ToString();
		[ForeignKey("UserId")]
		[Required]
		public string UserId { get; set; }
		public UserModel User { get; set; }
		[ForeignKey("PostId")]
		[Required]
		public string PostId { get; set; }
		public PostModel Post { get; set; }
	}
}
