using SocialMediaPlatform.Models;
using SocialMediaPlatform.Services.Interfaces;

namespace SocialMediaPlatform.Services
{
	public class CommentGetter : ICommentGetter
	{
		private readonly AppDbContext _Context;
		public CommentGetter(AppDbContext Context)
		{
			_Context = Context;
		}
		public async Task<List<CommentModel>> GetCommentsByPostId(string PostId)
		{
			return null;
		}
	}
}
