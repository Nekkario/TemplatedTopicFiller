using Abstractions.Logic;
using Microsoft.Extensions.Options;
using Models.Domain.Messages;
using Models.Transport.Configuration;

namespace Logic;

/// <summary>
/// Renderer of message templates.
/// </summary>
public sealed class MessageTemplateRenderer : IMessageTemplateRenderer
{
    private readonly string _keyTemplate;
    private readonly string _valueTemplate;
    private readonly ITemplateTokenValueGenerator _tokenValueGenerator;

    /// <summary>
    /// Creates a message template renderer.
    /// </summary>
    /// <param name="options">
    /// Message template configuration.
    /// </param>
    /// <param name="tokenValueGenerator">
    /// Template token value generator.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// If one of the parameters is <see langword="null"/>.
    /// </exception>
    public MessageTemplateRenderer(
        IOptions<MessageTemplateOptions> options,
        ITemplateTokenValueGenerator tokenValueGenerator)
    {
        ArgumentNullException.ThrowIfNull(options);
        ArgumentNullException.ThrowIfNull(tokenValueGenerator);

        _keyTemplate = options.Value.KeyTemplate;
        _valueTemplate = options.Value.ValueTemplate;
        _tokenValueGenerator = tokenValueGenerator;
    }

    /// <summary>
    /// Renders a message.
    /// </summary>
    /// <returns>
    /// Generated message.
    /// </returns>
    public GeneratedMessage Render()
    {
        return new GeneratedMessage
        {
            Key = _tokenValueGenerator.Generate(_keyTemplate),
            Value = _tokenValueGenerator.Generate(_valueTemplate)
        };
    }
}
