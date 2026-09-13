using Abstractions.Transport;
using Microsoft.Extensions.DependencyInjection;
using Models.Domain.Messages;
using Transport.Serialization;

namespace Transport;

/// <summary>
/// Transport layer registration.
/// </summary>
public static class TransportRegistrations
{
    /// <summary>
    /// Adds the transport layer.
    /// </summary>
    /// <param name="services">
    /// Service collection.
    /// </param>
    /// <returns>
    /// Service collection.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// If <paramref name="services"/> is <see langword="null"/>.
    /// </exception>
    public static IServiceCollection AddTransport(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        return services
            .AddSingleton<JsonGeneratedMessageSerializer>()
            .AddSingleton<IMessageSerializer<GeneratedMessage>>(
                static provider => provider.GetRequiredService<JsonGeneratedMessageSerializer>())
            .AddSingleton<IMessageDeserializer<GeneratedMessage>>(
                static provider => provider.GetRequiredService<JsonGeneratedMessageSerializer>())
            .AddSingleton<IMessagePublisher, KafkaMessagePublisher>();
    }
}
