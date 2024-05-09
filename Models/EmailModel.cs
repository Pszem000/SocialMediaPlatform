using System.ComponentModel.DataAnnotations;

namespace Messenger.Models
{
    public class EmailModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
