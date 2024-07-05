using Microsoft.AspNetCore.Mvc;
using SocialMediaPlatform.Models;
using SocialMediaPlatform.Services.Interfaces;

namespace SocialMediaPlatform.Controllers
{
	public class CommentsController : Controller
	{
		private readonly ICommentSaver _CommentSaver;
		public CommentsController(ICommentSaver commentSaver)
		{
			_CommentSaver = commentSaver;
		}
		public async Task AddComment(string Content, string PostId)
		{
			await _CommentSaver.AddComment(Content, PostId);
		}
	}
}
