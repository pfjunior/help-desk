using HD.Tickets.Api.Configuration;
using HD.WebApi.Core.Identitty;

var builder = WebApplication.CreateBuilder(args);

builder.AddApiConfiguration()
       .AddJwtConfiguration()
       .RegisterServices()
       .AddSwaggerConfiguration()
       .AddMessageBusConfiguration();


var app = builder.Build();

app.UseApiConfiguration();
app.MapControllers();

app.Run();
