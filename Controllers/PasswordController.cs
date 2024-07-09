using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMediaPlatform;
using SocialMediaPlatform.Models;
using SocialMediaPlatform.Services.Interfaces;

namespace SocialMediaPlatform.Controllers
{
	public class PasswordController : Controller
	{
		private readonly IUserService _UserService;
		private readonly UserManager<UserModel> _UserManager;
		private readonly AppDbContext _Context;
		private readonly IUserGetter _UserGetter;
		private readonly IRecoveryCodeGetter _RecoveryCodeGetter;
		private readonly IRecoveryCodeGenerator _RecoveryCodeGenerator;
		private readonly IEmailSender _EmailSender;
		private readonly IPasswordService _PasswordService;

		public PasswordController(IUserService UserService, IEmailSender EmailSender, IRecoveryCodeGenerator RecoveryCodeGenerator, IRecoveryCodeGetter RecoveryCodeGetter, UserManager<UserModel> UserManager, AppDbContext Context, IUserGetter UserGetter, IPasswordService PasswordService)
		{
			_UserManager = UserManager;
			_Context = Context;
			_UserGetter = UserGetter;
			_RecoveryCodeGetter = RecoveryCodeGetter;
			_RecoveryCodeGenerator = RecoveryCodeGenerator;
			_EmailSender = EmailSender;
			_UserService = UserService;
			_PasswordService = PasswordService;
		}
		public IActionResult Index()
		{
			return View();
		}
		[HttpGet]
		public IActionResult GetRecoveryCode()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> GetRecoveryCode(EmailModel EmailModel)
		{
			if (ModelState.IsValid)
			{
				await _EmailSender.SendEmail(EmailModel.Email);
				return RedirectToAction("ChangePasswordByRecoveryCode", "Password", new { EmailModel.Email });
			}
			return View(EmailModel);
		}
		[HttpGet]
		public IActionResult ChangePasswordByRecoveryCode(string Email)
		{
			var ChangePasswordModel = new ChangePasswordModel
			{
				Email = Email
			};
			return View(ChangePasswordModel);
		}
		[HttpPost]
		public async Task<IActionResult> ChangePasswordByRecoveryCode(ChangePasswordModel ChangePasswordModel)
		{
			if (ModelState.IsValid)
			{
				ViewBag.Error = await _PasswordService.ChangePasswordByRecoveryCode(ChangePasswordModel);
			}
			return View(ChangePasswordModel);
		}
		[HttpPost]
		public async Task<IActionResult> ChangePasswordByPassword(string CurrentPassword, string NewPassword, string UserId)
		{
			await _PasswordService.ChangePasswordByPassword(CurrentPassword, NewPassword, UserId);
			return Redirect("/Account");
		}
	}
}
