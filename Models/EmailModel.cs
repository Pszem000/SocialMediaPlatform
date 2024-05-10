using System.ComponentModel.DataAnnotations;

namespace SocialMediaPlatform.Models
{
	public class EmailModel
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }
	}
}
