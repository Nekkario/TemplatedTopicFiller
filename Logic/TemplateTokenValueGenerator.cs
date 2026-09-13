using System.Text.RegularExpressions;
using Abstractions.Logic;

namespace Logic;

/// <summary>
/// Generator of template token values.
/// </summary>
public sealed class TemplateTokenValueGenerator : ITemplateTokenValueGenerator
{
    private static readonly Regex TokenRegex =
        new(
            @"\b(?<token>int|long|double|string|bool|date)\b(?:\(\s*(?<min>-?\d+(?:\.\d+)?)\s*,\s*(?<max>-?\d+(?:\.\d+)?)\s*\))?",
            RegexOptions.Compiled | RegexOptions.CultureInvariant);

    private readonly IReadOnlyDictionary<string, ITemplateTokenGenerationStrategy> _strategiesByToken;

    /// <summary>
    /// Creates a template token value generator.
    /// </summary>
    /// <param name="strategies">
    /// Template token generation strategies.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// If <paramref name="strategies"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// If two strategies handle the same token.
    /// </exception>
    public TemplateTokenValueGenerator(IEnumerable<ITemplateTokenGenerationStrategy> strategies)
    {
        ArgumentNullException.ThrowIfNull(strategies);

        var dictionary = new Dictionary<string, ITemplateTokenGenerationStrategy>(StringComparer.OrdinalIgnoreCase);

        foreach (var strategy in strategies)
        {
            foreach (var token in strategy.Tokens)
            {
                if (!dictionary.TryAdd(token, strategy))
                {
                    throw new InvalidOperationException(
                        $"Duplicate strategy for token '{token}'.");
                }
            }
        }

        _strategiesByToken = dictionary;
    }

    /// <summary>
    /// Generates a value for a template.
    /// </summary>
    /// <param name="template">
    /// Message template.
    /// </param>
    /// <returns>
    /// Rendered template.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// If <paramref name="template"/> is <see langword="null"/>.
    /// </exception>
    public string Generate(string template)
    {
        ArgumentNullException.ThrowIfNull(template);

        return TokenRegex.Replace(
            template,
            match =>
            {
                var token = match.Groups["token"].Value;

                if (!_strategiesByToken.TryGetValue(token, out var strategy))
                {
                    return match.Value;
                }

                var context = new TemplateTokenGenerationContext
                {
                    Token = token,
                    Min = match.Groups["min"].Success ? match.Groups["min"].Value : null,
                    Max = match.Groups["max"].Success ? match.Groups["max"].Value : null
                };

                return strategy.Generate(context);
            });
    }
}
