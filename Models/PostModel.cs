using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMediaPlatform.Models
{
	public class PostModel
	{
		[Key]
		public string Id { get; set; } = Guid.NewGuid().ToString();
		[Required]
		public string Content { get; set; }
		[ForeignKey("CreatorId")]
		[Required]
		public string CreatorId { get; set; }
		public UserModel Creator { get; set; }
		public DateTime PublicationDate { get; set; } = DateTime.Now;
		public bool IsEdited {  get; set; }	
		public int NumberOfLikes {  get; set; }
		public int NumberOfComments { get; set; }
		public List<LikeModel> Likes { get; set; }
		public List<CommentModel> Comments { get; set; }
		public bool IsLikedByCurrentUser { get; set; }
	}
}
