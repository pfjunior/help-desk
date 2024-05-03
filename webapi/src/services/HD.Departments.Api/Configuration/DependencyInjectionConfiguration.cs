using HD.Departments.Api.Data;
using HD.Departments.Api.Data.Repository;
using HD.Departments.Api.Domain.Interfaces;

namespace HD.Departments.Api.Configuration;

public static class DependencyInjectionConfiguration
{
    public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
    {
        // Data
        builder.Services.AddScoped<DepartmentContext>();

        // Repository
        builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();


        return builder;
    }
}
