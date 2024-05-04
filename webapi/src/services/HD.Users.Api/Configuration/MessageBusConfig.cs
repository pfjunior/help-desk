using HD.Core.Utils;
using HD.MessageBus;
using HD.Users.Api.Services;

namespace HD.Users.Api.Configuration;

public static class MessageBusConfig
{
    public static WebApplicationBuilder AddMessageBusConfiguration(this WebApplicationBuilder builder)
    {
        builder.Services.AddMessageBus(builder.Configuration.GetMessageQueueConnection("MessageBus"))
            .AddHostedService<UserRegisterIntegrationHandler>();

        return builder;
    }
}
