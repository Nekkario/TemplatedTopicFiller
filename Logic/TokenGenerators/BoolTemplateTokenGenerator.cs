using Abstractions.Logic;

namespace Logic.TokenGenerators;

/// <summary>
/// Generator of boolean token values.
/// </summary>
public sealed class BoolTemplateTokenGenerator : ITemplateTokenGenerationStrategy
{
    /// <summary>
    /// Tokens handled by the generator.
    /// </summary>
    public IReadOnlyCollection<string> Tokens { get; } = new[] { "bool" };

    /// <summary>
    /// Generates a boolean token value.
    /// </summary>
    /// <param name="context">
    /// Token generation context.
    /// </param>
    /// <returns>
    /// Generated token value.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// If <paramref name="context"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// If the token specifies a range.
    /// </exception>
    public string Generate(TemplateTokenGenerationContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        NumericRangeParser.EnsureNoRange(context);

        return Random.Shared.Next(2) == 1 ? "true" : "false";
    }
}
