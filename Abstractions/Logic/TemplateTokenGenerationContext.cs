namespace Abstractions.Logic;

/// <summary>
/// Context of a template token generation.
/// </summary>
public sealed class TemplateTokenGenerationContext
{
    /// <summary>
    /// Token name.
    /// </summary>
    public string Token { get; init; } = string.Empty;

    /// <summary>
    /// Range lower bound.
    /// </summary>
    public string? Min { get; init; }

    /// <summary>
    /// Range upper bound.
    /// </summary>
    public string? Max { get; init; }
}
