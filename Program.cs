using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialMediaPlatform;
using SocialMediaPlatform.Models;
using SocialMediaPlatform.Services;
using SocialMediaPlatform.Services.Interfaces;


var builder = WebApplication.CreateBuilder(args);

var _AppSettings = builder.Configuration.GetSection("AppSettings").Get<AppSettings>();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
    options.KeepAliveInterval = TimeSpan.FromSeconds(20);
    options.ClientTimeoutInterval = TimeSpan.FromSeconds(20);
});

builder.Services.AddHttpClient();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IImageSaver, ImageSaver>();
builder.Services.AddScoped<IMessageGetter, MessageGetter>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IUserGetter, UserGetter>();
builder.Services.AddScoped<IRecoveryCodeGetter, RecoveryCodeGetter>();
builder.Services.AddScoped<IRecoveryCodeGenerator, RecoveryCodeGenerator>();
builder.Services.AddScoped<IPostsService, PostsService>();
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddScoped<IPostGetter, PostGetter>();
builder.Services.AddScoped<ILikeGetter, LikeGetter>();
builder.Services.AddScoped<ILikeService, LikeService>();
builder.Services.AddScoped<IFollowGetter, FollowGetter>();
builder.Services.AddScoped<IFollowService, FollowService>();
builder.Services.AddControllersWithViews();

builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
    options.TokenLifespan = TimeSpan.FromMinutes(10);
});
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7097/") });


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(_AppSettings.ConnectionString),
    ServiceLifetime.Scoped);
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
app.MapHub<AppHub>("/testhub");
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
