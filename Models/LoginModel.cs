using System.ComponentModel.DataAnnotations;

namespace Messenger.models
{
	public class LoginModel
	{
		[Required]
		public string UserName { get; set; }
		[Required]
		public string Password { get; set; }
	}
}
