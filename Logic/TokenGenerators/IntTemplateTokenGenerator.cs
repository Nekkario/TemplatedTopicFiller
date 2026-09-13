using Abstractions.Logic;

namespace Logic.TokenGenerators;

/// <summary>
/// Generator of integer token values.
/// </summary>
public sealed class IntTemplateTokenGenerator : ITemplateTokenGenerationStrategy
{
    /// <summary>
    /// Tokens handled by the generator.
    /// </summary>
    public IReadOnlyCollection<string> Tokens { get; } = new[] { "int" };

    /// <summary>
    /// Generates an integer token value.
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
    public string Generate(TemplateTokenGenerationContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        var (min, max) = NumericRangeParser.ParseIntRange(context, defaultMin: 1, defaultMax: 1000);
        var value = Random.Shared.NextInt64(min, (long)max + 1);

        return value.ToString();
    }
}
