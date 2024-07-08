using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using reCAPTCHA.AspNetCore;
using SocialMediaPlatform.Models;
using SocialMediaPlatform.Services.Interfaces;
using System.Configuration;

namespace SocialMediaPlatform.Services
{
	public class RegisterService : IRegistserService
	{
		private readonly AppDbContext _Context;
		private readonly UserManager<UserModel> _UserManager;
		private readonly IImageSaver _ImageSaver;
		private readonly SignInManager<UserModel> _SignInManager;
		private readonly string DefaultImagePath;
		private readonly string SecretKey;
		private readonly IRecaptchaValidator _RecaptchaValidator;

		public RegisterService(IConfiguration Configuration, AppDbContext context, UserManager<UserModel> userManager, IImageSaver imageSaver, SignInManager<UserModel> signInManager, IRecaptchaValidator recaptchaValidator)
		{
			_Context = context;
			_UserManager = userManager;
			_ImageSaver = imageSaver;
			_SignInManager = signInManager;
			DefaultImagePath = Configuration.GetValue<string>("AppSettings:DefaultImagePath");
			SecretKey = Configuration.GetValue<string>("AppSettings:ReCAPTCHA_SecretKey");
			_RecaptchaValidator = recaptchaValidator;
		}


		public async Task<string> Register(RegisterModel UserData, IFormFile? ProfileImage, string recaptchaToken)
		{
			var CaptchaIsValid = await _RecaptchaValidator.ValidateRecaptcha(recaptchaToken);
			var NewUser = new UserModel
			{
				Email = UserData.EmailAdress,
				UserName = UserData.UserName,
				Bio = ""
			};

			if (_Context.Users.Where(User => User.Email == UserData.EmailAdress).ToList().Count != 0)
			{
				return $"Email {UserData.EmailAdress} is already taken";
			}

			var Result = await _UserManager.CreateAsync(NewUser, UserData.Password);

			if (Result.Succeeded)
			{
				if (ProfileImage != null && ProfileImage.Length > 0)
				{
					await _ImageSaver.SaveImage(ProfileImage, NewUser.Id);
				}
				else
				{
					NewUser.ProfileImageSrc = DefaultImagePath;
					await _Context.SaveChangesAsync();
				}

				await _SignInManager.PasswordSignInAsync(UserData.UserName, UserData.Password, false, false);
				return null;
			}
			return Result.Errors.FirstOrDefault().Description;
		}
	}
}
