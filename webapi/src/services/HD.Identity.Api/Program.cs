using HD.Identity.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.AddApiConfiguration()
       .AddIdentityConfiguration()
       .AddSwaggerConfiguration();

var app = builder.Build();

app.UseApiConfiguration();
app.MapControllers();

app.Run();
