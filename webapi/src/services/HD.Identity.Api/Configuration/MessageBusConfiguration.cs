using HD.Core.Utils;
using HD.MessageBus;

namespace HD.Identity.Api.Configuration;

public static class MessageBusConfiguration
{
    public static WebApplicationBuilder AddMessageBusConfiguration(this WebApplicationBuilder builder)
    {
        builder.Services.AddMessageBus(builder.Configuration.GetMessageQueueConnection("MessageBus"));

        return builder;
    }
}
