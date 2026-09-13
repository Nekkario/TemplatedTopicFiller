using System.Text.Json;
using Abstractions.Transport;
using Models.Domain.Messages;

namespace Transport.Serialization;

/// <summary>
/// JSON serializer and deserializer of generated messages.
/// </summary>
public sealed class JsonGeneratedMessageSerializer :
    IMessageSerializer<GeneratedMessage>,
    IMessageDeserializer<GeneratedMessage>
{
    /// <summary>
    /// Serializes a message.
    /// </summary>
    /// <param name="message">
    /// Message.
    /// </param>
    /// <returns>
    /// JSON representation of the message value.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// If <paramref name="message"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="JsonException">
    /// If the message JSON representation is invalid.
    /// </exception>
    public string Serialize(GeneratedMessage message)
    {
        ArgumentNullException.ThrowIfNull(message);

        using var document = JsonDocument.Parse(message.Value);

        return message.Value;
    }

    /// <summary>
    /// Deserializes a message.
    /// </summary>
    /// <param name="key">
    /// Message key.
    /// </param>
    /// <param name="value">
    /// JSON representation of the message value.
    /// </param>
    /// <returns>
    /// Generated message.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// If one of the parameters is <see langword="null"/>.
    /// </exception>
    /// <exception cref="JsonException">
    /// If the message JSON representation is invalid.
    /// </exception>
    public GeneratedMessage Deserialize(string key, string value)
    {
        ArgumentNullException.ThrowIfNull(key);
        ArgumentNullException.ThrowIfNull(value);

        using var document = JsonDocument.Parse(value);

        return new GeneratedMessage
        {
            Key = key,
            Value = value
        };
    }
}
