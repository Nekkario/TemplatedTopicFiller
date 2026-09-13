using Abstractions.Transport;
using Confluent.Kafka;
using Microsoft.Extensions.Options;
using Models.Domain.Messages;
using Models.Transport.Configuration;

namespace Transport;

/// <summary>
/// Publisher of messages to Kafka.
/// </summary>
public sealed class KafkaMessagePublisher : IMessagePublisher, IDisposable
{
    private readonly IProducer<string, string> _producer;
    private readonly KafkaProducerOptions _options;
    private readonly IMessageSerializer<GeneratedMessage> _messageSerializer;

    /// <summary>
    /// Creates a Kafka message publisher.
    /// </summary>
    /// <param name="options">
    /// Kafka producer configuration.
    /// </param>
    /// <param name="messageSerializer">
    /// Message serializer.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// If one of the parameters is <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// If the Kafka producer configuration is invalid.
    /// </exception>
    public KafkaMessagePublisher(
        IOptions<KafkaProducerOptions> options,
        IMessageSerializer<GeneratedMessage> messageSerializer)
    {
        ArgumentNullException.ThrowIfNull(options);
        ArgumentNullException.ThrowIfNull(messageSerializer);

        _options = options.Value;
        _messageSerializer = messageSerializer;

        if (string.IsNullOrWhiteSpace(_options.BootstrapServers))
        {
            throw new InvalidOperationException("Kafka bootstrap servers are not configured.");
        }

        if (string.IsNullOrWhiteSpace(_options.TopicName))
        {
            throw new InvalidOperationException("Kafka topic name is not configured.");
        }

        var producerConfig = new ProducerConfig
        {
            BootstrapServers = _options.BootstrapServers,
            SecurityProtocol = ParseSecurityProtocol(_options.SecurityProtocol)
        };

        _producer = new ProducerBuilder<string, string>(producerConfig).Build();
    }

    /// <summary>
    /// Publishes a message.
    /// </summary>
    /// <param name="message">
    /// Message.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// Message publication task.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// If <paramref name="message"/> is <see langword="null"/>.
    /// </exception>
    public async Task PublishAsync(GeneratedMessage message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);

        var kafkaMessage = new Message<string, string>
        {
            Key = message.Key,
            Value = _messageSerializer.Serialize(message)
        };

        _ = await _producer.ProduceAsync(_options.TopicName, kafkaMessage, cancellationToken);
    }

    /// <summary>
    /// Releases the publisher resources.
    /// </summary>
    public void Dispose()
    {
        _producer.Flush(TimeSpan.FromSeconds(10));
        _producer.Dispose();
    }

    private static SecurityProtocol ParseSecurityProtocol(string protocol)
    {
        if (Enum.TryParse<SecurityProtocol>(protocol, ignoreCase: true, out var value))
        {
            return value;
        }

        throw new InvalidOperationException(
            $"Kafka security protocol '{protocol}' is invalid.");
    }
}
