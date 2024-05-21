﻿using Microsoft.EntityFrameworkCore;
using SocialMediaPlatform.Models;
using SocialMediaPlatform.Services.Interfaces;

namespace SocialMediaPlatform.Services
{
	public class LikeService : ILikeService
	{
		private readonly AppDbContext _Context;
		private readonly IPostGetter _PostGetter;
		private readonly IUserGetter _UserGetter;
		public LikeService(AppDbContext Context, IPostGetter PostGetter, IUserGetter UserGetter)
		{
			_Context = Context;
			_PostGetter = PostGetter;
			_UserGetter = UserGetter;
		}
		public async Task AddLikeModel(string PostId,string UserId)
		{
			var LikeModel = new LikeModel { PostId = PostId, UserId = UserId };
			await _Context.LikeList.AddAsync(LikeModel);
			var Post = await _PostGetter.GetPostsById(PostId);
			var User = await _UserGetter.GetUserById(UserId);
			Post.Likes.Add(User);
			Post.NumberOfLikes++;
			await _Context.SaveChangesAsync();	
		}
		public async Task RemoveLikeModel(string PostId, string UserId)
		{
			var Like = await _Context.LikeList.Where(x=>x.UserId == UserId && x.PostId == PostId).FirstOrDefaultAsync();
			if(Like != null) 
			{
				_Context.LikeList.Remove(Like);
				var Post = await _PostGetter.GetPostsById(PostId);
				var User = await _UserGetter.GetUserById(UserId);
				Post.Likes.Remove(User);
				Post.NumberOfLikes--;
				await _Context.SaveChangesAsync();
			}
		
			
		}
	}
}
