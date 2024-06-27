﻿using SocialMediaPlatform.Models;

namespace SocialMediaPlatform.Services.Interfaces
{
	public interface IPostsService
	{
		public Task AddPost(PostModel Post);
		public Task RemovePost(string PostId);
		public Task EditPostContent(string NewContnet, string PostId);
	}
}
