using Microsoft.EntityFrameworkCore;
using SocialMediaPlatform.Models;
using SocialMediaPlatform.Services.Interfaces;

namespace SocialMediaPlatform.Services
{
	public class FollowService : IFollowService
	{
		private readonly AppDbContext _Context;
		private readonly IUserGetter _UserGetter;
		public FollowService(AppDbContext Context, IUserGetter UserGetter)
		{
			_Context = Context;
			_UserGetter = UserGetter;
		}
		public async Task AddFollow(string FollowerId, string FollowedId)
		{
			var FollowModel = new FollowModel
			{
				FollowedId = FollowedId,
				FollowerId = FollowerId
			};
			await _Context.Follows.AddAsync(FollowModel);
			var Follower = await _UserGetter.GetUserById(FollowerId);
			var Followed = await _UserGetter.GetUserById(FollowedId);
			Follower.NumberOfFollowing++;
			Followed.NumberOfFollowers++;
			await _Context.SaveChangesAsync();
		}
		public async Task RemoveFollow(string FollowerId,string FollowedId)
		{
			var FollowModel = await _Context.Follows.Where(x => x.FollowerId == FollowerId && x.FollowedId == FollowedId).FirstOrDefaultAsync();
			if(FollowModel != null)
			{
				_Context.Follows.Remove(FollowModel);
				var Follower = await _UserGetter.GetUserById(FollowerId);			
				Follower.NumberOfFollowers--;
				var Followed = await _UserGetter.GetUserById(FollowedId);
				Followed.NumberOfFollowing--;
				await _Context.SaveChangesAsync();
			}
		}
	}
}
