using Polly;
using Polly.Extensions.Http;
using Polly.Retry;

namespace HD.WebApi.Core.Extensions;

public static class PollyExtensions
{
    public static AsyncRetryPolicy<HttpResponseMessage> WaitTry() =>
        HttpPolicyExtensions.HandleTransientHttpError().WaitAndRetryAsync(3, retryAttempts => TimeSpan.FromSeconds(Math.Pow(2, retryAttempts)));
}
