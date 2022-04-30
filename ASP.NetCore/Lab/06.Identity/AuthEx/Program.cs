using AuthEx.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Add services to the container.
services.AddControllersWithViews();

IConfiguration configuration = builder.Configuration;

var connstr = configuration.GetConnectionString("DefaultConnection");
services.AddDbContext<AuthExDBContext>(options => options.UseSqlServer(connstr));

services.AddScoped<DataSeeder>();

services.AddIdentity<IdentityUser, IdentityRole>()
              .AddEntityFrameworkStores<AuthExDBContext>()
              .AddDefaultTokenProviders();

services.AddAuthentication()
  .AddCookie();

services.ConfigureApplicationCookie(config =>
{
    config.AccessDeniedPath = "/Auth/UnAuthorized";
    config.LoginPath = "/Auth/Login";
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//seed the database
using (var scope = app.Services.CreateScope())
{
    var seed
        = scope.ServiceProvider.GetService<DataSeeder>();
    await seed.CreateRolesAsync();
}

app.Run();
