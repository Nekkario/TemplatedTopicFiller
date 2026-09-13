using System.Text;
using Abstractions.Logic;

namespace Logic.TokenGenerators;

/// <summary>
/// Generator of string token values.
/// </summary>
public sealed class StringTemplateTokenGenerator : ITemplateTokenGenerationStrategy
{
    private const string Alphabet = "abcdefghijklmnopqrstuvwxyz";

    /// <summary>
    /// Tokens handled by the generator.
    /// </summary>
    public IReadOnlyCollection<string> Tokens { get; } = new[] { "string" };

    /// <summary>
    /// Generates a string token value.
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

        var length = Random.Shared.Next(5, 13);
        var value = new StringBuilder(length);

        for (var index = 0; index < length; index++)
        {
            var character = Alphabet[Random.Shared.Next(Alphabet.Length)];
            value.Append(character);
        }

        return value.ToString();
    }
}
