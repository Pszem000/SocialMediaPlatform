
using Messenger.Services.Interfaces;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace Messenger.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly string _EmailLogin;
        private readonly string _EmailPassword;
        public EmailSender(IConfiguration Configuration)
        {
            _EmailLogin = Configuration.GetValue<string>("AppSettings:EmailLogin");
			_EmailPassword = Configuration.GetValue<string>("AppSettings:EmailPassword");
		}
        public Task SendEmail(string Email, string RecoveryCode)
        {
            var MailClient = new SmtpClient("smtp-mail.outlook.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(_EmailLogin, _EmailPassword)
            };
            var Subject = "Password recovery code";
            var Content = $"Your password recovery code: {RecoveryCode}";

            return MailClient.SendMailAsync(
               new MailMessage(
                   from: _EmailLogin,
                   to: Email,
                   Subject,
                   Content
                   ));
        }
    }
}
