using Microsoft.EntityFrameworkCore;
using PicturePilot.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PicturesDbContext>(options =>
{
    options.UseSqlite("Data Source=PicturesDbContext.db");
    SQLitePCL.Batteries.Init();
});
builder.Services.AddControllersWithViews();
builder.Services.AddMvc();

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();
app.MapControllerRoute(name: "default", pattern: "{controller=Admin}/{action=Index}");

app.Run();
