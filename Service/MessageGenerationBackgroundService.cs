using Abstractions.Logic;
using Abstractions.Transport;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Models.Transport.Configuration;

namespace Service;

/// <summary>
/// Background service that generates and publishes messages.
/// </summary>
public sealed class MessageGenerationBackgroundService : BackgroundService
{
    private readonly IMessageTemplateRenderer _messageTemplateRenderer;
    private readonly IMessagePublisher _messagePublisher;
    private readonly IOptions<MessageOutputOptions> _outputOptions;
    private readonly IHostApplicationLifetime _applicationLifetime;

    /// <summary>
    /// Creates a message generation background service.
    /// </summary>
    /// <param name="messageTemplateRenderer">
    /// Message template renderer.
    /// </param>
    /// <param name="messagePublisher">
    /// Message publisher.
    /// </param>
    /// <param name="outputOptions">
    /// Message output configuration.
    /// </param>
    /// <param name="applicationLifetime">
    /// Application lifetime.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// If one of the parameters is <see langword="null"/>.
    /// </exception>
    public MessageGenerationBackgroundService(
        IMessageTemplateRenderer messageTemplateRenderer,
        IMessagePublisher messagePublisher,
        IOptions<MessageOutputOptions> outputOptions,
        IHostApplicationLifetime applicationLifetime)
    {
        ArgumentNullException.ThrowIfNull(messageTemplateRenderer);
        ArgumentNullException.ThrowIfNull(messagePublisher);
        ArgumentNullException.ThrowIfNull(outputOptions);
        ArgumentNullException.ThrowIfNull(applicationLifetime);

        _messageTemplateRenderer = messageTemplateRenderer;
        _messagePublisher = messagePublisher;
        _outputOptions = outputOptions;
        _applicationLifetime = applicationLifetime;
    }

    /// <summary>
    /// Generates and publishes messages.
    /// </summary>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// Message generation task.
    /// </returns>
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        for (var index = 0; index < _outputOptions.Value.Count; index++)
        {
            var message = _messageTemplateRenderer.Render();
            await _messagePublisher.PublishAsync(message, cancellationToken);
        }

        _applicationLifetime.StopApplication();
    }
}
