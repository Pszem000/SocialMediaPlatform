using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents;
using Newtonsoft.Json;
using reCAPTCHA.AspNetCore;
using SocialMediaPlatform;
using SocialMediaPlatform.Models;
using SocialMediaPlatform.Services.Interfaces;
using System.Configuration;
using System.Net;


namespace SocialMediaPlatform.Controllers
{
	public class AccountController : Controller
	{
		private readonly IUserService _UserService;
		private readonly UserManager<UserModel> _UserManager;
		private readonly SignInManager<UserModel> _SignInManager;
		private readonly AppDbContext _Context;
		private readonly IImageSaver _ImageSaver;
		private readonly IUserGetter _UserGetter;
		private readonly IRecoveryCodeGetter _RecoveryCodeGetter;
		private readonly IRecoveryCodeGenerator _RecoveryCodeGenerator;
		private readonly IEmailSender _EmailSender;
		private readonly IRecaptchaService _RecaptchaService;
		private readonly string DefaultImagePath;
		private readonly string SecretKey;


		public AccountController(IEmailSender EmailSender, IRecoveryCodeGenerator RecoveryCodeGenerator, IRecoveryCodeGetter RecoveryCodeGetter, IUserService UserService, UserManager<UserModel> UserManager, SignInManager<UserModel> SignInManager, AppDbContext Context, IImageSaver ImageSaver, IUserGetter UserGetter, IConfiguration Configuration, IRecaptchaService RecaptchaService)
		{
			_UserManager = UserManager;
			_SignInManager = SignInManager;
			_Context = Context;
			_ImageSaver = ImageSaver;
			_UserService = UserService;
			_UserGetter = UserGetter;
			_RecoveryCodeGetter = RecoveryCodeGetter;
			_RecoveryCodeGenerator = RecoveryCodeGenerator;
			_EmailSender = EmailSender;
			_RecaptchaService = RecaptchaService;
			DefaultImagePath = Configuration.GetValue<string>("AppSettings:DefaultImagePath");
			SecretKey = Configuration.GetValue<string>("AppSettings:ReCAPTCHA_SecretKey");
		}
		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Register(RegisterModel UserData, IFormFile? ProfileImage, string recaptchaToken)
		{

            var CaptchaIsValid = await ValidateRecaptcha(recaptchaToken);
            if (ModelState.IsValid && CaptchaIsValid)
			{
				var NewUser = new UserModel
				{
					Email = UserData.EmailAdress,
					UserName = UserData.UserName,
				};

				if (_Context.Users.Where(User => User.Email == UserData.EmailAdress).ToList().Count != 0)
				{
					ViewBag.Error = $"Email {UserData.EmailAdress} is already taken";
					return View();
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

					return RedirectToAction("Index", "Home");
				}
				else
				{
					ViewBag.Error = Result.Errors.FirstOrDefault().Description;
				}

			}
			return View();
		}
		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Login(LoginModel UserData, string recaptchaToken)
		{
			var CaptchaIsValid = await ValidateRecaptcha(recaptchaToken);
			if (ModelState.IsValid && CaptchaIsValid)
			{
				var Result = await _SignInManager.PasswordSignInAsync(UserData.UserName, UserData.Password, false, false);
				if (Result.Succeeded)
				{
					return Redirect("/");
				}
				else
				{
					ViewBag.Error = "Username or password is wrong";
				}
			}
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> LogOut()
		{
			await _SignInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}
		private async Task<bool> ValidateRecaptcha(string recaptchaToken)
		{
			var httpClient = new HttpClient();
			var response = await httpClient.PostAsync($"https://www.google.com/recaptcha/api/siteverify?secret={SecretKey}&response={recaptchaToken}", null);
			var responseString = await response.Content.ReadAsStringAsync();
			var recaptchaResponse = JsonConvert.DeserializeObject<RecaptchaResponse>(responseString);
			return recaptchaResponse.success;
		}

	}
}

