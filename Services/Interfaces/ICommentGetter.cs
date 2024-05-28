using SocialMediaPlatform.Models;

namespace SocialMediaPlatform.Services.Interfaces
{
	public interface ICommentGetter
	{
		public Task<List<CommentModel>> GetCommentsByPostId(string PostId);
	}
}
