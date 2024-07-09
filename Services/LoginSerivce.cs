using Microsoft.AspNetCore.Identity;
using SocialMediaPlatform.Models;
using SocialMediaPlatform.Services.Interfaces;

namespace SocialMediaPlatform.Services
{
	public class LoginSerivce : ILoginService
	{
		private readonly IRecaptchaValidator _RecaptchaValidator;
		private readonly SignInManager<UserModel> _SignInManager;

		public LoginSerivce(IRecaptchaValidator recaptchaValidator, SignInManager<UserModel> signInManager)
		{
			_RecaptchaValidator = recaptchaValidator;
			_SignInManager = signInManager;
		}

		public async Task<string> Login(LoginModel UserData, string recaptchaToken)
		{
			var CaptchaIsValid = await _RecaptchaValidator.ValidateRecaptcha(recaptchaToken);
			var Result = await _SignInManager.PasswordSignInAsync(UserData.UserName, UserData.Password, false, false);
			if (!Result.Succeeded)
			{
				return "Username or password is wrong";
			}
			return null;
		}
	}
}
