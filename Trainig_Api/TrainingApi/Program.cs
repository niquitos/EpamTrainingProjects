using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using Serilog.Sinks.Loki;

namespace TrainingApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog((context,cfg)=> 
                {
                    var credentials = new NoAuthCredentials(context.Configuration["ConnectionStrings:Loki"]);

                    cfg.MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                        .Enrich.FromLogContext()
                        .Enrich.WithProperty("Application", context.HostingEnvironment.ApplicationName)
                        .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                        .WriteTo.Console(new RenderedCompactJsonFormatter())
                        .WriteTo.LokiHttp(credentials);
                });
        }
    }
}
