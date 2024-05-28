using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMediaPlatform.Models
{
	public class CommentModel
	{
		[Key]
		public string Id { get; set; } = Guid.NewGuid().ToString();
		[Required]
		public string Content { get; set; }
		public DateTime PublicationDate { get; set; } = DateTime.Now;
		[ForeignKey(nameof(CreatorId))]

		public string CreatorId { get; set; }
		public UserModel Creator { get; set; }
		[ForeignKey(nameof(PostId))]
		public string PostId { get; set; }
		public PostModel Post { get; set; }
	}
}
