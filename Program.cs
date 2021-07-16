using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace TrainingApi
{
    /// <summary>
    /// Starts the application
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Application entry points
        /// </summary>
        /// <param name="args">Array of arguments</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Program initialization method
        /// </summary>
        /// <param name="args">Array of arguments passed from Main</param>
        /// <returns>A program initialization abstraction</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
