using HD.Users.Api.Data;
using HD.Users.Api.Data.Repository;
using HD.Users.Api.Domain.Interfaces;

namespace HD.Users.Api.Configuration;

public static class DependencyInjectionConfiguration
{
    public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
    {
        // Data
        builder.Services.AddScoped<UserContext>();

        // Repository
        builder.Services.AddScoped<IUserRepository, UserRepository>();


        return builder;
    }
}
