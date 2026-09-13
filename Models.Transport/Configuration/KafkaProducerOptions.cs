namespace Models.Transport.Configuration;

/// <summary>
/// Kafka producer configuration.
/// </summary>
public sealed class KafkaProducerOptions
{
    /// <summary>
    /// Kafka broker addresses.
    /// </summary>
    public string BootstrapServers { get; set; } = string.Empty;

    /// <summary>
    /// Kafka topic name.
    /// </summary>
    public string TopicName { get; set; } = string.Empty;

    /// <summary>
    /// Kafka security protocol.
    /// </summary>
    public string SecurityProtocol { get; set; } = "PLAINTEXT";

    /// <summary>
    /// Kafka consumer group identifier.
    /// </summary>
    public string ConsumerGroupId { get; set; } = "my-csharp-consumer";
}
