using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SocialMediaPlatform.Models;

namespace SocialMediaPlatform
{
	public class AppDbContext : IdentityDbContext<UserModel>
	{
		public AppDbContext()
		{

		}

		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{

		}


		public DbSet<ImageModel> ImageList { get; set; }

		public DbSet<MessageModel> MessageList { get; set; }
		public DbSet<PostModel> Posts { get; set; }
		public DbSet<LikeModel> LikeList { get; set; }
		public DbSet<CommentModel> Comments { get; set; }
		public DbSet<FollowModel> Follows { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ImageModel>()
			.Ignore(e => e.Tag);

			modelBuilder.Entity<IdentityUserLogin<string>>().HasNoKey();
			modelBuilder.Entity<IdentityUserRole<string>>().HasNoKey();
			modelBuilder.Entity<IdentityUserToken<string>>().HasNoKey();

			modelBuilder.Entity<LikeModel>()
				.HasOne(l => l.User)
				.WithMany(u => u.Likes)
				.HasForeignKey(l => l.UserId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<LikeModel>()
				.HasOne(l => l.Post)
				.WithMany(p => p.Likes)
				.HasForeignKey(l => l.PostId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<UserModel>()
				.HasMany(u => u.Likes)
				.WithOne(l => l.User)
				.HasForeignKey(l => l.UserId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<PostModel>()
				.HasMany(p => p.Likes)
				.WithOne(l => l.Post)
				.HasForeignKey(l => l.PostId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<FollowModel>()
				.HasOne(f => f.Follower)
				.WithMany(u => u.FollowedUsers)
				.HasForeignKey(f => f.FollowerId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<FollowModel>()
				.HasOne(f => f.Followed)
				.WithMany(u => u.Followers)
				.HasForeignKey(f => f.FollowedId)
				.OnDelete(DeleteBehavior.Restrict);

		}
	}
}
