﻿using HD.Identity.Api.Data;
using HD.WebApi.Core.Identitty;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HD.Identity.Api.Configuration;

public static class IdentityConfiguration
{
    public static WebApplicationBuilder AddIdentityConfiguration(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                //.AddErrorDescriber<IdentityPortuguseMessages>()
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();

        builder.Services.AddJwtConfiguration(builder.Configuration);

        return builder;
    }
}
