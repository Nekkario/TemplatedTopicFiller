namespace Models.Transport.Configuration;

/// <summary>
/// Message template configuration.
/// </summary>
public sealed class MessageTemplateOptions
{
    /// <summary>
    /// Message key template.
    /// </summary>
    public string KeyTemplate { get; set; } = string.Empty;

    /// <summary>
    /// Message value template.
    /// </summary>
    public string ValueTemplate { get; set; } = string.Empty;
}
