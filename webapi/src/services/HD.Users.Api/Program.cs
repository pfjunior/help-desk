using HD.Users.Api.Configuration;
using HD.WebApi.Core.Identitty;

var builder = WebApplication.CreateBuilder(args);

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
