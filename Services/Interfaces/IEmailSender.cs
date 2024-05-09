namespace Messenger.Services.Interfaces
{
    public interface IEmailSender
    {
        public Task SendEmail(string Email, string RecoveryCode);
    }
}
