using AutoMapper;
using GreatWorld.Mappings;
using GreatWorld.Data;
using GreatWorld.Models;
using GreatWorld.Repository;
using GreatWorld.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Mvc;

string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
    builder =>
    {
        builder.WithOrigins("http://example.com",
                                "http://www.contoso.com")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
    });
});

// Add services to the container.
//DataContractSerializer serializes or deserializes
//AddXmlDataContractSerializerFormatters adds support for formatting of
//serialized or deserialized data as xml
services.AddControllersWithViews(options =>
{
    options.RespectBrowserAcceptHeader = true; // false by default

    options.CacheProfiles.Add("Default30", new CacheProfile
    {
        Duration = 30, //duration (in seconds)
        Location = ResponseCacheLocation.Any //means cached to client and proxies. Sets  'cache-control' header in response.
    });

})
.AddXmlDataContractSerializerFormatters()
.AddXmlSerializerFormatters();

var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

IMapper mapper = mappingConfig.CreateMapper();
services.AddSingleton(mapper);

services.AddTransient<GreatWorldContextSeedData>();

IConfiguration configuration = builder.Configuration;

var connstr = configuration.GetConnectionString("DefaultConnection");
services.AddDbContext<GreatWorldWithEFContext>(options => options.UseSqlServer(connstr));

services.AddScoped<IMailService, DebugMailService>(); //to remain available until session
                                                         //If we ask for DebugMailServicetwice in a single request(even from different components
                                                         //like our Controller and View), the exact same instance will be returned
services.AddTransient<TestClass>();

services.AddScoped<IGreatWorldRepository, GreatWorldRepository>();

services.AddIdentity<WorldUser, IdentityRole>(cfg =>
{
    cfg.User.RequireUniqueEmail = true;
    cfg.Password.RequiredLength = 8;
})
        .AddEntityFrameworkStores<GreatWorldWithEFContext>();

services.AddAuthentication()
        .AddCookie()
        .AddJwtBearer(cfg =>
        {
            cfg.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidIssuer = configuration["Tokens:Issuer"],
                ValidAudience = configuration["Tokens:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Tokens:Key"]))
            };
        });


services.ConfigureApplicationCookie(config =>
{
    config.LoginPath = "/Auth/Login";
});

//services.AddResponseCaching();
services.AddMemoryCache();


var app = builder.Build();

//UseResponseCaching before UseStaticFiles
//app.UseResponseCaching();

//app.UseDefaultFiles();
app.UseStaticFiles();

app.UseCors(MyAllowSpecificOrigins);

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=App}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var seed
        = scope.ServiceProvider.GetService<GreatWorldContextSeedData>();
    await seed.EnsureSeedDataAsync();
}

app.Run();