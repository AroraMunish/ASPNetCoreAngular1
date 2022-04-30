using GreatWorld.Models;
using GreatWorld.Services;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

// Add services to the container.
services.AddControllersWithViews();

services.AddScoped<IMailService, DebugMailService>(); //to remain available until session
                                                         //If we ask for DebugMailServicetwice in a single request(even from different components
                                                         //like our Controller and View), the exact same instance will be returned
services.AddTransient<TestClass>();

var app = builder.Build();

//app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=App}/{action=Index}/{id?}");

app.Run();
