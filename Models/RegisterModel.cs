using System.ComponentModel.DataAnnotations;

namespace Messenger.models
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
