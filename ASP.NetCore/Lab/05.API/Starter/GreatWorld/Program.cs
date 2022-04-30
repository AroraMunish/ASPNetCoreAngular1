using GreatWorld.Data;
using GreatWorld.Models;
using GreatWorld.Repository;
using GreatWorld.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

// Add services to the container.
services.AddControllersWithViews();

services.AddTransient<GreatWorldContextSeedData>();

IConfiguration configuration = builder.Configuration;

var connstr = configuration.GetConnectionString("DefaultConnection");
services.AddDbContext<GreatWorldWithEFContext>(options => options.UseSqlServer(connstr));

services.AddScoped<IMailService, DebugMailService>(); //to remain available until session
                                                         //If we ask for DebugMailServicetwice in a single request(even from different components
                                                         //like our Controller and View), the exact same instance will be returned
services.AddTransient<TestClass>();

services.AddScoped<IGreatWorldRepository, GreatWorldRepository>();

var app = builder.Build();

//app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=App}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var seed
        = scope.ServiceProvider.GetService<GreatWorldContextSeedData>();
    seed.EnsureSeedData();
}

app.Run();
