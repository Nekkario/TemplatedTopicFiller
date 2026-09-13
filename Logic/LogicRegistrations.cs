using Abstractions.Logic;
using Logic.TokenGenerators;
using Microsoft.Extensions.DependencyInjection;

namespace Logic;

/// <summary>
/// Logic layer registration.
/// </summary>
public static class LogicRegistrations
{
    /// <summary>
    /// Adds the logic layer.
    /// </summary>
    /// <param name="services">
    /// Service collection.
    /// </param>
    /// <returns>
    /// Service collection.
    /// </returns>
    public static IServiceCollection AddLogic(this IServiceCollection services)
    {
        return services
            .AddSingleton<IMessageTemplateRenderer, MessageTemplateRenderer>()
            .AddSingleton<ITemplateTokenValueGenerator, TemplateTokenValueGenerator>()
            .AddSingleton<ITemplateTokenGenerationStrategy, IntTemplateTokenGenerator>()
            .AddSingleton<ITemplateTokenGenerationStrategy, LongTemplateTokenGenerator>()
            .AddSingleton<ITemplateTokenGenerationStrategy, DoubleTemplateTokenGenerator>()
            .AddSingleton<ITemplateTokenGenerationStrategy, StringTemplateTokenGenerator>()
            .AddSingleton<ITemplateTokenGenerationStrategy, BoolTemplateTokenGenerator>()
            .AddSingleton<ITemplateTokenGenerationStrategy, DateTemplateTokenGenerator>();
    }
}
