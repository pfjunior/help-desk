using HD.Core.Mediator;
using HD.Tickets.Domain.Interfaces;
using HD.Tickets.Infra.Data;
using HD.Tickets.Infra.Data.Repository;
using HD.WebApi.Core.User;

namespace HD.Tickets.Api.Configuration;

public static class DependencyInjectionConfiguration
{
    public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
    {
        // API
        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        builder.Services.AddScoped<IAspNetUser, AspNetUser>();

        // Data
        builder.Services.AddScoped<TicketContext>();
        builder.Services.AddScoped<ITicketRepository, TicketRepository>();

        // Application
        builder.Services.AddScoped<IMediatorHandler, MediatorHandler>();


        return builder;
    }
}