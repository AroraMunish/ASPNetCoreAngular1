//create application builder
var builder = WebApplication.CreateBuilder(args);

//build the application
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.Run((context) =>
  {
    throw new Exception("Example exception");
  });
}

//Add router endpoint
app.MapGet("/", () => "Hello World!");

//Run the application
app.Run();
