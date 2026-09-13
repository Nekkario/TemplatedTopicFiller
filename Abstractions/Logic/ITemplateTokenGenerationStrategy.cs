namespace Abstractions.Logic;

/// <summary>
/// Represents a template token generation strategy.
/// </summary>
public interface ITemplateTokenGenerationStrategy
{
    /// <summary>
    /// Tokens handled by the strategy.
    /// </summary>
    IReadOnlyCollection<string> Tokens { get; }

    /// <summary>
    /// Generates a token value.
    /// </summary>
    /// <param name="context">
    /// Token generation context.
    /// </param>
    /// <returns>
    /// Generated token value.
    /// </returns>
    string Generate(TemplateTokenGenerationContext context);
}
