using HD.Infra.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace HD.Api.Configurations;

public static class DbContextConfig
{
    public static WebApplicationBuilder AddDbContextConfig(this WebApplicationBuilder builder)
    {
        var connStringBuilder = new SqlConnectionStringBuilder(builder.Configuration.GetConnectionString("DefaultConnection"));
        connStringBuilder.Password = builder.Configuration["DbPassword"];
        builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connStringBuilder.ConnectionString));

        return builder;
    }
}
