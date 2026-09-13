namespace Abstractions.Transport;

/// <summary>
/// Represents a message deserializer.
/// </summary>
/// <typeparam name="TMessage">
/// Message.
/// </typeparam>
public interface IMessageDeserializer<out TMessage>
{
    /// <summary>
    /// Deserializes a message.
    /// </summary>
    /// <param name="key">
    /// Message key.
    /// </param>
    /// <param name="value">
    /// Message value.
    /// </param>
    /// <returns>
    /// Deserialized message.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// If one of the parameters is <see langword="null"/>.
    /// </exception>
    TMessage Deserialize(string key, string value);
}
