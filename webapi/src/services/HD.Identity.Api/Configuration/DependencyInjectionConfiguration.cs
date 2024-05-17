using HD.Identity.Api.Services;
using HD.WebApi.Core.User;

namespace HD.Identity.Api.Configuration;

public static class DependencyInjectionConfiguration
{
    public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
    {
        // API
        builder.Services.AddScoped<IAspNetUser, AspNetUser>();

        // Services
        builder.Services.AddScoped<AuthenticationService>();

        return builder;
    }
}
