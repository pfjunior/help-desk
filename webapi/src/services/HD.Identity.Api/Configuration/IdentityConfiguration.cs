using HD.Identity.Api.Data;
using HD.Identity.Api.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Security.Jwt.Core.Jwa;

namespace HD.Identity.Api.Configuration;

public static class IdentityConfiguration
{
    public static WebApplicationBuilder AddIdentityConfiguration(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<AppTokenSettings>(builder.Configuration.GetSection("AppTokenSettings"));

        builder.Services.AddJwksManager(options => options.Jws = Algorithm.Create(DigitalSignaturesAlgorithm.EcdsaSha256))
                        .PersistKeysToDatabaseStore<ApplicationContext>()
                        .UseJwtValidation();

        builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddDefaultIdentity<IdentityUser>()
                        .AddRoles<IdentityRole>()
                        //.AddErrorDescriber<IdentityPortuguseMessages>()
                        .AddEntityFrameworkStores<ApplicationContext>()
                        .AddDefaultTokenProviders();

        return builder;
    }
}
