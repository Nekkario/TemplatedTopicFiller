using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Service;

var host = Host.CreateDefaultBuilder(args)
    .UseContentRoot(AppContext.BaseDirectory)
    .ConfigureAppConfiguration((_, configurationBuilder) =>
    {
        configurationBuilder
            .AddJsonFile(
                Path.Combine(AppContext.BaseDirectory, "appsettings.json"),
                optional: false,
                reloadOnChange: false)
            .AddEnvironmentVariables();
    })
    .ConfigureServices((context, services) => services.AddService(context.Configuration))
    .Build();

await host.RunAsync();
