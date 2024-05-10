using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMediaPlatform.Models
{
	public class MessageModel
	{


		[Key]
		public int Id { get; set; }
		[Required]
		public string Content { get; set; }
		[Required]
		public string CreatorId { get; set; }
		[Required]
		public string ReciverId { get; set; }
		[ForeignKey("ReciverId")]
		public UserModel Receiver { get; set; }
		public bool IsRead { get; set; }
		public DateTime SendTime { private set; get; } = DateTime.Now;
	}



}
