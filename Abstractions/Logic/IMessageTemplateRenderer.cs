using Models.Domain.Messages;

namespace Abstractions.Logic;

/// <summary>
/// Represents a renderer of message templates.
/// </summary>
public interface IMessageTemplateRenderer
{
    /// <summary>
    /// Renders a message.
    /// </summary>
    /// <returns>
    /// Generated message.
    /// </returns>
    GeneratedMessage Render();
}
