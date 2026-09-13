namespace Abstractions.Logic;

/// <summary>
/// Represents a generator of template token values.
/// </summary>
public interface ITemplateTokenValueGenerator
{
    /// <summary>
    /// Generates a value for a template.
    /// </summary>
    /// <param name="template">
    /// Message template.
    /// </param>
    /// <returns>
    /// Rendered template.
    /// </returns>
    string Generate(string template);
}
