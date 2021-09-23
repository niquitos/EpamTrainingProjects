using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RabbitWindowsService
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                        .UseWindowsService()
                        .ConfigureServices((hostContext, services) =>
                        {
                            var config = hostContext.Configuration;
                            services.AddOptions();
                            services.Configure<RabbitMqConfiguration>(config.GetSection("RabbitMq"));
                            services.AddHostedService<EmployeeConsumerService>();
                        });
        }
    }
}
