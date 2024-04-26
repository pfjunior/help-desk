using HD.Users.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.AddApiConfiguration()
       .RegisterServices()
       .AddSwaggerConfiguration();

var app = builder.Build();

app.UseApiConfiguration();
app.MapControllers();

app.Run();
