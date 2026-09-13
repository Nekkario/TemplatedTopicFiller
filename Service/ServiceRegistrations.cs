using Logic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Models.Transport.Configuration;
using Transport;

namespace Service;

/// <summary>
/// Service layer registration.
/// </summary>
public static class ServiceRegistrations
{
    /// <summary>
    /// Adds the service layer.
    /// </summary>
    /// <param name="services">
    /// Service collection.
    /// </param>
    /// <param name="configuration">
    /// Application configuration.
    /// </param>
    public static void AddService(this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddLogic()
            .AddTransport()
            .Configure<MessageTemplateOptions>(configuration.GetSection("MessageTemplate"))
            .Configure<MessageOutputOptions>(configuration.GetSection("MessageSettings"))
            .Configure<KafkaProducerOptions>(configuration.GetSection("Kafka"))
            .AddHostedService<MessageGenerationBackgroundService>();
    }
}
