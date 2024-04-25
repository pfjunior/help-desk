using HD.WebApi.Core.Identitty;

namespace HD.Identity.Api.Configuration;

public static class ApiConfiguration
{
    public static WebApplicationBuilder AddApiConfiguration(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        return builder;
    }

    public static WebApplication UseApiConfiguration(this WebApplication app)
    {
        if (app.Environment.IsDevelopment()) app.UseSwaggerConfiguration();

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthConfiguration();

        return app;
    }
}
