using System.Threading;
using System.Threading.Tasks;

namespace RabbitWindowsService
{
    public interface IEmployeeConsumerService
    {
        Task StartAsync(CancellationToken cancellationToken);
        Task StopAsync(CancellationToken cancellationToken);
    }
}