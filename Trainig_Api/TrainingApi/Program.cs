using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TrainingApi.Services.Messages;

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
                        .UseWindowsService()
                        .ConfigureServices((hostContext, services) =>
                        {
                            var config = hostContext.Configuration;
                            services.Configure<RabbitMqConfiguration>(config.GetSection("RabbitMq"));
                            services.AddHostedService<EmployeeConsumerService>();
                        });
        }
    }
}
