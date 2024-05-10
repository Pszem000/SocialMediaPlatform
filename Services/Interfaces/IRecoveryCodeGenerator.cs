namespace SocialMediaPlatform.Services.Interfaces
{
	public interface IRecoveryCodeGenerator
	{
		public Task ChangeRecoveryCode(string UserId);
	}
}
