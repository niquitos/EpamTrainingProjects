using System.Threading.Tasks;

namespace RabbitWindowsService
{
    public interface IRabbitConsumer
    {
        Task StartAsync();
        void Stop();
    }
}