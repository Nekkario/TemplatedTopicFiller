using System.Globalization;
using Abstractions.Logic;

namespace Logic.TokenGenerators;

/// <summary>
/// Generator of double token values.
/// </summary>
public sealed class DoubleTemplateTokenGenerator : ITemplateTokenGenerationStrategy
{
    /// <summary>
    /// Tokens handled by the generator.
    /// </summary>
    public IReadOnlyCollection<string> Tokens { get; } = new[] { "double" };

    /// <summary>
    /// Generates a double token value.
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
    /// If the range is too large.
    /// </exception>
    public string Generate(TemplateTokenGenerationContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        var (min, max) = NumericRangeParser.ParseDoubleRange(context, defaultMin: 0.0, defaultMax: 5.0);

        try
        {
            checked
            {
                var minScaled = (long)Math.Round(min * 1_000d, MidpointRounding.AwayFromZero);
                var maxScaled = (long)Math.Round(max * 1_000d, MidpointRounding.AwayFromZero);

                var scaledValue = Random.Shared.NextInt64(minScaled, maxScaled + 1);
                var value = scaledValue / 1_000d;

                return value.ToString("0.000", CultureInfo.InvariantCulture);
            }
        }
        catch (OverflowException exception)
        {
            throw new ArgumentOutOfRangeException(
                "Range for token 'double' is too large.",
                exception);
        }
    }
}
