using HD.Domain.Core.Interfaces;
using HD.Domain.Core.Notifications;
using HD.Domain.Departments.Interfaces;
using HD.Domain.Departments.Services;
using HD.Domain.Tickets.Interfaces;
using HD.Domain.Tickets.Services;
using HD.Domain.Users.Interfaces;
using HD.Domain.Users.Services;
using HD.Infra.Context;
using HD.Infra.Repositories;

namespace HD.Api.Configurations;

public static class DependencyInjectionConfig
{
    public static IServiceCollection ResolveDependencies(this IServiceCollection services)
    {
        #region Infra
        services.AddScoped<ApplicationContext>();
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITicketRepository, TicketRepository>();
        #endregion

        #region Domain
        services.AddScoped<INotifier, Notifier>();
        services.AddScoped<IDepartmentService, DepartmentService>();
        services.AddScoped<UserService, UserService>();
        services.AddScoped<ITicketService, TicketService>();
        #endregion

        return services;
    }
}
