using Microsoft.Azure.Documents;
using SocialMediaPlatform.Models;
using SocialMediaPlatform.Services.Interfaces;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace SocialMediaPlatform.Services
{
	public class EmailSender : IEmailSender
	{
		private readonly string _EmailLogin;
		private readonly string _EmailPassword;
		private readonly IUserGetter _UserGetter;
		private readonly IRecoveryCodeGenerator _RecoveryCodeGenerator;
		private readonly IRecoveryCodeGetter _RecoveryCodeGetter;
		public EmailSender(IConfiguration Configuration, IUserGetter UserGetter, IRecoveryCodeGenerator RecoveryCodeGenerator, IRecoveryCodeGetter RecoveryCodeGetter)
		{
			_EmailLogin = Configuration.GetValue<string>("AppSettings:EmailLogin");
			_EmailPassword = Configuration.GetValue<string>("AppSettings:EmailPassword");
			_UserGetter = UserGetter;
			_RecoveryCodeGenerator = RecoveryCodeGenerator;
			_RecoveryCodeGetter = RecoveryCodeGetter;
		}
		public Task SendEmail(string Email)
		{
			var MailClient = new SmtpClient("smtp-mail.outlook.com", 587)
			{
				EnableSsl = true,
				Credentials = new NetworkCredential(_EmailLogin, _EmailPassword)
			};
			var User = _UserGetter.GetUserByEmail(Email).Result;
			_RecoveryCodeGenerator.ChangeRecoveryCode(User.Id);
			var RecoveryCode = _RecoveryCodeGetter.GetRecoveryCode(User.Id);
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
