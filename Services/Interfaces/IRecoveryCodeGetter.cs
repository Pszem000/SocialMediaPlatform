namespace Messenger.Services.Interfaces
{
    public interface IRecoveryCodeGetter
    {
        public string GetRecoveryCode(string UserId);
        public Task<string> GenerateRecoveryCode(string UserId);

    }
}
