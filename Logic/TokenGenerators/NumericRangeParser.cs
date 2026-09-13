using System.Globalization;
using Abstractions.Logic;

namespace Logic.TokenGenerators;

/// <summary>
/// Parser of numeric token ranges.
/// </summary>
internal static class NumericRangeParser
{
    /// <summary>
    /// Parses an integer range.
    /// </summary>
    /// <param name="context">
    /// Token generation context.
    /// </param>
    /// <param name="defaultMin">
    /// Default range lower bound.
    /// </param>
    /// <param name="defaultMax">
    /// Default range upper bound.
    /// </param>
    /// <returns>
    /// Parsed range.
    /// </returns>
    /// <exception cref="FormatException">
    /// If a range value is not a valid int.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// If the range lower bound is greater than the upper bound.
    /// </exception>
    public static (int Min, int Max) ParseIntRange(TemplateTokenGenerationContext context, int defaultMin, int defaultMax)
    {
        if (!HasRange(context))
        {
            return (defaultMin, defaultMax);
        }

        var min = ParseInt(context.Min!, context.Token);
        var max = ParseInt(context.Max!, context.Token);
        ValidateRange(min, max, context.Token);

        return (min, max);
    }

    /// <summary>
    /// Parses a long range.
    /// </summary>
    /// <param name="context">
    /// Token generation context.
    /// </param>
    /// <param name="defaultMin">
    /// Default range lower bound.
    /// </param>
    /// <param name="defaultMax">
    /// Default range upper bound.
    /// </param>
    /// <returns>
    /// Parsed range.
    /// </returns>
    /// <exception cref="FormatException">
    /// If a range value is not a valid long.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// If the range lower bound is greater than the upper bound.
    /// </exception>
    public static (long Min, long Max) ParseLongRange(
        TemplateTokenGenerationContext context,
        long defaultMin,
        long defaultMax)
    {
        if (!HasRange(context))
        {
            return (defaultMin, defaultMax);
        }

        var min = ParseLong(context.Min!, context.Token);
        var max = ParseLong(context.Max!, context.Token);
        ValidateRange(min, max, context.Token);

        return (min, max);
    }

    /// <summary>
    /// Parses a double range.
    /// </summary>
    /// <param name="context">
    /// Token generation context.
    /// </param>
    /// <param name="defaultMin">
    /// Default range lower bound.
    /// </param>
    /// <param name="defaultMax">
    /// Default range upper bound.
    /// </param>
    /// <returns>
    /// Parsed range.
    /// </returns>
    /// <exception cref="FormatException">
    /// If a range value is not a valid double.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// If the range lower bound is greater than the upper bound.
    /// </exception>
    public static (double Min, double Max) ParseDoubleRange(
        TemplateTokenGenerationContext context,
        double defaultMin,
        double defaultMax)
    {
        if (!HasRange(context))
        {
            return (defaultMin, defaultMax);
        }

        var min = ParseDouble(context.Min!, context.Token);
        var max = ParseDouble(context.Max!, context.Token);
        ValidateRange(min, max, context.Token);

        return (min, max);
    }

    /// <summary>
    /// Ensures a token has no range.
    /// </summary>
    /// <param name="context">
    /// Token generation context.
    /// </param>
    /// <exception cref="InvalidOperationException">
    /// If the token specifies a range.
    /// </exception>
    public static void EnsureNoRange(TemplateTokenGenerationContext context)
    {
        if (!HasRange(context))
        {
            return;
        }

        throw new InvalidOperationException($"Token '{context.Token}' does not support range syntax.");
    }

    private static bool HasRange(TemplateTokenGenerationContext context)
    {
        return context.Min is not null || context.Max is not null;
    }

    private static int ParseInt(string value, string token)
    {
        if (int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var parsed))
        {
            return parsed;
        }

        throw new FormatException($"Token '{token}' range value '{value}' is not a valid int.");
    }

    private static long ParseLong(string value, string token)
    {
        if (long.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var parsed))
        {
            return parsed;
        }

        throw new FormatException($"Token '{token}' range value '{value}' is not a valid long.");
    }

    private static double ParseDouble(string value, string token)
    {
        if (double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var parsed))
        {
            return parsed;
        }

        throw new FormatException($"Token '{token}' range value '{value}' is not a valid double.");
    }

    private static void ValidateRange<T>(T min, T max, string token)
        where T : IComparable<T>
    {
        if (min.CompareTo(max) <= 0)
        {
            return;
        }

        throw new ArgumentOutOfRangeException(
            nameof(min),
            $"Invalid range for token '{token}': min value is greater than max value.");
    }
}
