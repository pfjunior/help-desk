using HD.Departments.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

builder.AddApiConfiguration()
       .RegisterServices()
       .AddSwaggerConfiguration();

var app = builder.Build();

app.UseApiConfiguration();
app.MapControllers();

app.Run();