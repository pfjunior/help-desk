using HD.Identity.Api.Configuration;
using HD.WebApi.Core.Identitty;

var builder = WebApplication.CreateBuilder(args);

builder.AddApiConfiguration()
       .AddIdentityConfiguration()
       .AddJwtConfiguration()
       .AddSwaggerConfiguration()
       .AddMessageBusConfiguration();

var app = builder.Build();

app.UseApiConfiguration();
app.MapControllers();

app.Run();
