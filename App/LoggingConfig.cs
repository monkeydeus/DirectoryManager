using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace App
{
    public static class LoggingConfig
    {
        public static ServiceProvider ConfigureLogging()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            return serviceCollection.BuildServiceProvider();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("log.txt")
                .CreateLogger();

            services.AddLogging(loggingBuilder =>
                    loggingBuilder.AddSerilog(dispose: true))
                .Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Information);
        }
    }
}