using Microsoft.AspNetCore.Mvc;
using SocialMediaPlatform.Models;

namespace SocialMediaPlatform.Services.Interfaces
{
	public interface IPasswordService
	{
		public Task<string> ChangePasswordByRecoveryCode(ChangePasswordModel ChangePasswordModel);
		public Task<string> ChangePasswordByPassword(string CurrentPassword, string NewPassword, string UserId);
	}
}
