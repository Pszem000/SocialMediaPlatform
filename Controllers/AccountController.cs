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
		private readonly IRegistserService _RegisterService;


		public AccountController(IEmailSender EmailSender, IRegistserService registserService, IRecoveryCodeGenerator RecoveryCodeGenerator, IRecoveryCodeGetter RecoveryCodeGetter, IUserService UserService, UserManager<UserModel> UserManager, SignInManager<UserModel> SignInManager, AppDbContext Context, IImageSaver ImageSaver, IUserGetter UserGetter, IConfiguration Configuration, IRecaptchaService RecaptchaService)
		{
			_RegisterService = registserService;
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
			if (ModelState.IsValid)
			{
				ViewBag.Error = await _RegisterService.Register(UserData, ProfileImage, recaptchaToken);
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

	}
}

