using System.ComponentModel.DataAnnotations;

namespace SocialMediaPlatform.Models
{
	public class RegisterModel
	{
		[Required]
		public string UserName { get; set; }
		[Required]
		[EmailAddress]
		public string EmailAdress { get; set; }
		[Required]
		public string Password { get; set; }
	}
}
