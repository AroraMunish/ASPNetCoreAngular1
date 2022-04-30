var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Add services to the container.
services.AddControllersWithViews();

services.AddDistributedMemoryCache();
services.AddSession(config =>
{
    config.IdleTimeout = TimeSpan.FromMinutes(5);
});


var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Cookies}/{action=Index}/{id?}");

app.Run();
