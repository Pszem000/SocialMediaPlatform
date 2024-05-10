using Microsoft.EntityFrameworkCore;
using SocialMediaPlatform.Services.Interfaces;

namespace SocialMediaPlatform.Services
{
	public class RecoveryCodeGenerator : IRecoveryCodeGenerator
	{
		private readonly IUserGetter _UserGetter;
		private readonly AppDbContext _Context;
		public RecoveryCodeGenerator(IUserGetter UserGetter, AppDbContext Context)
		{
			_UserGetter = UserGetter;
			_Context = Context;
		}

		public async Task ChangeRecoveryCode(string UserId)
		{
			var User = await _UserGetter.GetUserById(UserId);
			if (User != null)
			{
				var Code = GenerateRecoveryCode();
				User.RecoveryCode = Code;
				await _Context.SaveChangesAsync();
			}
		}
		private string GenerateRecoveryCode()
		{
			Guid Guid = Guid.NewGuid();
			string Code = Guid.ToString();
			Code = Code.Replace("-", "").Substring(0, 10);
			return Code;
		}

	}
}
