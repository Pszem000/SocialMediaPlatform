using System.ComponentModel.DataAnnotations;

namespace SocialMediaPlatform.Models
{
	public class ChangePasswordModel
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }
		[Required]
		public string NewPassword { get; set; }
		[Required]
		public string ConfirmPassword { get; set; }
		[Required]
		public string RecoveryCode { get; set; }

	}
}
