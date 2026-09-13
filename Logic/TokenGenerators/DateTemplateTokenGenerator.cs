using Abstractions.Logic;

namespace Logic.TokenGenerators;

/// <summary>
/// Generator of date token values.
/// </summary>
public sealed class DateTemplateTokenGenerator : ITemplateTokenGenerationStrategy
{
    /// <summary>
    /// Tokens handled by the generator.
    /// </summary>
    public IReadOnlyCollection<string> Tokens { get; } = new[] { "date" };

    /// <summary>
    /// Generates a date token value.
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

        return DateTime.UtcNow.Date
            .AddDays(Random.Shared.Next(1, 30))
            .ToString("yyyy-MM-dd");
    }
}
