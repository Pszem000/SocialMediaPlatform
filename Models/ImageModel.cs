using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SocialMediaPlatform.Models
{
	public class ImageModel
	{
		[Key]
		public int Id { get; set; }
		public byte[] image { get; set; }
		public string ContentType { get; set; }
		[ForeignKey("UserId")]
		public string UserId { get; set; }
		public UserModel User { get; set; }
		[ForeignKey("MessageId")]
		public string? MessageId { get; set; }
		public MessageModel? Message { get; set; }
		[NotMapped]
		public string Tag { get; set; }
	}
}