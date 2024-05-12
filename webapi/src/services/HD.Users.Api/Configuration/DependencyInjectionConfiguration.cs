using FluentValidation.Results;
using HD.Core.Mediator;
using HD.Users.Api.Application.Commands;
using HD.Users.Api.Application.Events;
using HD.Users.Api.Data;
using HD.Users.Api.Data.Repository;
using HD.Users.Api.Domain.Interfaces;
using HD.WebApi.Core.User;
using MediatR;

namespace HD.Users.Api.Configuration;

public static class DependencyInjectionConfiguration
{
    public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
    {
        // API
        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        builder.Services.AddScoped<IAspNetUser, AspNetUser>();

        // Data
        builder.Services.AddScoped<UserContext>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();

        // Mediatr
        builder.Services.AddScoped<IMediatorHandler, MediatorHandler>();

        // Command
        builder.Services.AddScoped<IRequestHandler<UserRegisterCommand, ValidationResult>, UserCommandHandler>();

        // Event
        builder.Services.AddScoped<INotificationHandler<UserRegistredEvent>, UserEventHandler>();

        return builder;
    }
}
