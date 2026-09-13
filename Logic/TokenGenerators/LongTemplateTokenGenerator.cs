using Abstractions.Logic;

namespace Logic.TokenGenerators;

/// <summary>
/// Generator of long token values.
/// </summary>
public sealed class LongTemplateTokenGenerator : ITemplateTokenGenerationStrategy
{
    /// <summary>
    /// Tokens handled by the generator.
    /// </summary>
    public IReadOnlyCollection<string> Tokens { get; } = new[] { "long" };

    /// <summary>
    /// Generates a long token value.
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
    /// <exception cref="ArgumentOutOfRangeException">
    /// If the range upper bound is too large.
    /// </exception>
    public string Generate(TemplateTokenGenerationContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        var (min, max) = NumericRangeParser.ParseLongRange(
            context,
            defaultMin: 1_000_000_000L,
            defaultMax: 9_000_000_000_000L);

        if (max == long.MaxValue)
        {
            throw new ArgumentOutOfRangeException(
                nameof(context),
                "Range upper bound for token 'long' must be less than long.MaxValue.");
        }

        var value = Random.Shared.NextInt64(min, max + 1);
        return value.ToString();
    }
}
