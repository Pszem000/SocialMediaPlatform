using Microsoft.EntityFrameworkCore;
using SocialMediaPlatform.Models;
using SocialMediaPlatform.Services.Interfaces;

namespace SocialMediaPlatform.Services
{
	public class FollowGetter : IFollowGetter
	{
		private readonly AppDbContext _Context;
		public FollowGetter(AppDbContext Context)
		{
			_Context = Context;
		}
		public async Task<FollowModel> GetCurrentFollow(string FollowerId, string FollowedId)
		{
			var FollowModel = await _Context.Follows.Where(x => x.FollowerId == FollowerId && x.FollowedId == FollowedId).Include(x => x.Followed).Include(x => x.Follower).FirstOrDefaultAsync();
			return FollowModel;
		}
		public async Task<List<FollowModel>> GetFollowersCurrentUser(string UserId)
		{
			var Follows = await _Context.Follows.Where(x => x.FollowedId == UserId).Include(x => x.Follower).Include(x => x.Followed).ToListAsync();
			return Follows;
		}
		public async Task<List<FollowModel>> GetFollowsCurrentUser(string UserId)
		{
			var Follows = await _Context.Follows.Where(x => x.FollowerId == UserId).Include(x => x.Follower).Include(x => x.Followed).ToListAsync();
			return Follows;
		}
	}
}
