using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using reCAPTCHA.AspNetCore;
using SocialMediaPlatform;
using SocialMediaPlatform.Models;
using SocialMediaPlatform.Services;
using SocialMediaPlatform.Services.Interfaces;
using System.Configuration;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddSignalR(options =>
{
	options.EnableDetailedErrors = true;
	options.KeepAliveInterval = TimeSpan.FromSeconds(20);
	options.ClientTimeoutInterval = TimeSpan.FromSeconds(20);
});

builder.Services.AddHttpClient();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IImageSaver, ImageSaver>();
builder.Services.AddTransient<IMessageGetter, MessageGetter>();
builder.Services.AddTransient<IMessageService, MessageService>();
builder.Services.AddTransient<IUserGetter, UserGetter>();
builder.Services.AddTransient<IRecoveryCodeGetter, RecoveryCodeGetter>();
builder.Services.AddTransient<IRecoveryCodeGenerator, RecoveryCodeGenerator>();
builder.Services.AddTransient<IPostsService, PostsService>();
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddTransient<IPostGetter, PostGetter>();
builder.Services.AddTransient<ILikeGetter, LikeGetter>();
builder.Services.AddTransient<ILikeService, LikeService>();
builder.Services.AddTransient<IFollowGetter, FollowGetter>();
builder.Services.AddTransient<IFollowService, FollowService>();
builder.Services.AddTransient<IBioService, BioService>();
builder.Services.AddTransient<ICommentSaver, CommentSaver>();
builder.Services.AddTransient<IUsernameChanger, UsernameChanger>();
builder.Services.AddTransient<IRegistserService, RegisterService>();
builder.Services.AddTransient<IRecaptchaValidator, RecaptchaValidator>();
builder.Services.AddTransient<ILoginService, LoginSerivce>();
builder.Services.AddTransient<IPasswordService, PasswordService>();
builder.Services.AddControllersWithViews();

builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
	options.TokenLifespan = TimeSpan.FromMinutes(10);
});
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7097/") });


builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetValue<string>("AppSettings:ConnectionString")), ServiceLifetime.Scoped);
builder.Services.AddIdentity<UserModel, IdentityRole>(options =>
{
	options.Password.RequireDigit = false;
	options.Password.RequiredLength = 2;
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequireLowercase = false;
	options.Password.RequireUppercase = false;
}).AddEntityFrameworkStores<AppDbContext>()
  .AddDefaultTokenProviders()
  .AddTokenProvider<DataProtectorTokenProvider<UserModel>>("TokenProvider");

builder.Services.AddRecaptcha(options =>
{
	options.SiteKey = builder.Configuration.GetValue<string>("AppSettings:ReCAPTCHA_SiteKey");
	options.SecretKey = builder.Configuration.GetValue<string>("AppSettings:ReCAPTCHA_SecretKey");
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.MapRazorPages();
app.UseRouting();
app.MapHub<AppHub>("/AppHub");
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
