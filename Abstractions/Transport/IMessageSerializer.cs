namespace Abstractions.Transport;

/// <summary>
/// Represents a message serializer.
/// </summary>
/// <typeparam name="TMessage">
/// Message.
/// </typeparam>
public interface IMessageSerializer<in TMessage>
{
    /// <summary>
    /// Serializes a message.
    /// </summary>
    /// <param name="message">
    /// Message.
    /// </param>
    /// <returns>
    /// Serialized message.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// If <paramref name="message"/> is <see langword="null"/>.
    /// </exception>
    string Serialize(TMessage message);
}
