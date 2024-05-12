using HD.Users.Api.Configuration;
using HD.WebApi.Core.Identitty;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

builder.AddApiConfiguration()
       .AddJwtConfiguration()
       .AddSwaggerConfiguration()
       .RegisterServices()
       .AddMessageBusConfiguration();

builder.Services.AddMediatR(options => options.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));


var app = builder.Build();

app.UseApiConfiguration();
app.MapControllers();

app.Run();
