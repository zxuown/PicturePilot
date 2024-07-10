using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PicturePilot.Data;
using PicturePilot.Data.Entities;
using PicturePilot.Data.Repositories;
using PicturePilot.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PicturesDbContext>(options =>
{
    options.UseSqlite("Data Source=PicturesDbContext.db");
    SQLitePCL.Batteries.Init();
});
builder.Services.AddControllersWithViews();
builder.Services.AddMvc();
builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
{
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
})
    .AddEntityFrameworkStores<PicturesDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddScoped<ImageService>();
builder.Services.AddScoped<ReportRepository>();
builder.Services.AddScoped<ImageRepository>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddAuthentication();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(120);
});
var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();
app.MapControllerRoute(name: "default", pattern: "{controller=Admin}/{action=Index}");

app.Run();
