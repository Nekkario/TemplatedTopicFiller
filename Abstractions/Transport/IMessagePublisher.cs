using Models.Domain.Messages;

namespace Abstractions.Transport;

/// <summary>
/// Represents a message publisher.
/// </summary>
public interface IMessagePublisher
{
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
    Task PublishAsync(GeneratedMessage message, CancellationToken cancellationToken);
}
