using Messenger.models;
using Messenger.Models;
using Messenger.Services;
using Messenger.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Controllers
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

		public PasswordController(IUserService UserService, IEmailSender EmailSender, IRecoveryCodeGenerator RecoveryCodeGenerator, IRecoveryCodeGetter RecoveryCodeGetter, UserManager<UserModel> UserManager, AppDbContext Context, IUserGetter UserGetter)
		{
			_UserManager = UserManager;
			_Context = Context;
			_UserGetter = UserGetter;
			_RecoveryCodeGetter = RecoveryCodeGetter;
			_RecoveryCodeGenerator = RecoveryCodeGenerator;
			_EmailSender = EmailSender;
			_UserService = UserService;
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

				var User = await _UserGetter.GetUserByEmail(EmailModel.Email);
				if (User != null)
				{
					await _RecoveryCodeGenerator.ChangeRecoveryCode(User.Id);
					var RecoveryCode = _RecoveryCodeGetter.GetRecoveryCode(User.Id);
					await _EmailSender.SendEmail(EmailModel.Email, RecoveryCode);
					return RedirectToAction("ChangePasswordByRecoveryCode", "Password", new { Email = EmailModel.Email });
				}
				else
				{
					ViewBag.Error = $"User with email {EmailModel.Email} does not  exist";
				}
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
				if (ChangePasswordModel.NewPassword == ChangePasswordModel.ConfirmPassword)
				{
					var User = await _UserGetter.GetUserByEmail(ChangePasswordModel.Email);

					var UserRecoveryCode = _RecoveryCodeGetter.GetRecoveryCode(User.Id);

					if (UserRecoveryCode == ChangePasswordModel.RecoveryCode)
					{
						await _UserService.ChangePassword(User, ChangePasswordModel.NewPassword);
						return Redirect("/");
					}
					else
					{
						ViewBag.Error = "Recovery code is wrong";
					}
				}
				else
				{
					ViewBag.Error = "Passwords are not the same";
				}
			}
			return View(ChangePasswordModel);
		}
		[HttpPost]
		public async Task<IActionResult> ChangePasswordByPassword(string CurrentPassword, string NewPassword, string UserId)
		{
			var User = await _UserGetter.GetUserById(UserId);
			if (User != null)
			{
				if (await _UserManager.CheckPasswordAsync(User, CurrentPassword))
				{
					var Result = await _UserManager.ChangePasswordAsync(User, CurrentPassword, NewPassword);
					if (Result.Succeeded)
					{
						await _Context.SaveChangesAsync();

					}
				}
			}
			return Redirect("/");

		}
	}
}
