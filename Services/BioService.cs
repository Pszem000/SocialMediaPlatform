using SocialMediaPlatform.Services.Interfaces;

namespace SocialMediaPlatform.Services
{
	public class BioService : IBioService
	{
		private readonly IUserGetter _UserGetter;
		private readonly AppDbContext _Context;
		public BioService(IUserGetter UserGetter, AppDbContext Context)
		{
			_UserGetter = UserGetter;
			_Context = Context;
		}

		public async Task ChangeBio(string Bio, string UserId)
		{
			var User = await _UserGetter.GetUserById(UserId);
			if (User != null)
			{
				User.Bio = Bio;
				await _Context.SaveChangesAsync();
			}

		}
	}
}
