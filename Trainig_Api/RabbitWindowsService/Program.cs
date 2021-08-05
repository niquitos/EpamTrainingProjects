using System;
using Topshelf;

namespace RabbitWindowsService
{
    class Program
    {
        static void Main(string[] args)
        {
            var exitCode = HostFactory.Run(x =>
            {
                x.Service<RabbitConsumer>(s =>
                {
                    s.ConstructUsing(consumer => new RabbitConsumer("amqp://guest:guest@localhost:5672","EmployeeQueue"));
                    s.WhenStarted(consumer => consumer.Start());
                    s.WhenStopped(consumer => consumer.Stop());
                });

                x.RunAsLocalSystem();

                x.SetServiceName("RabbitConsumerService");
                x.SetDisplayName("Rabbit Consumer Service");
                x.SetDescription(@"Windows service that listens to RabbitMq messages");
            });

            int exitCodeValue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());
            Environment.ExitCode = exitCodeValue;
        }
    }
}
