var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//app.MapGet("/", () => "Hello World!");
app.MapGet("/", () => ("Worker Process Name:" 
                        +System.Diagnostics.Process.GetCurrentProcess().ProcessName));

app.Run();
