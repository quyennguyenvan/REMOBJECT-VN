using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using System;

namespace Coffe_Order
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Log.Logger = SetStaticLogger();
                CreateHostBuilder(args).Build().Run();
            }
            catch(Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog(SetupLogger)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static void SetupLogger(HostBuilderContext hostBuilderContext, LoggerConfiguration loggerConfiguration)
        {
            var configuration = hostBuilderContext.Configuration.GetSection("SerialLog");
            if (bool.TrueString.Equals(configuration["RenderJson"],StringComparison.OrdinalIgnoreCase))
            {
                loggerConfiguration.WriteTo.Console(new RenderedCompactJsonFormatter());
            }
            else
            {
                loggerConfiguration.WriteTo.Console(outputTemplate: configuration["OutputTemplate"],
                    restrictedToMinimumLevel: Enum.Parse<LogEventLevel>(configuration["MinimumLevel"]));

                loggerConfiguration.WriteTo.File("applogs.txt", outputTemplate: configuration["OutputTemplate"], 
                    restrictedToMinimumLevel: Enum.Parse<LogEventLevel>(configuration["MinimumLevel"]));
            }
            loggerConfiguration.Enrich.FromLogContext();
        }
        private static ILogger SetStaticLogger()
        {
            return new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("applogs.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }
    }
}
