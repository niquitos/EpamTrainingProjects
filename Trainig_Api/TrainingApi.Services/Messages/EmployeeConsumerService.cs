using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.ComponentModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TrainingApi.Services.Messages
{
    public class EmployeeConsumerService : BackgroundWorker, IEmployeeConsumerService, IHostedService
    {
        private readonly string _uri;
        private readonly string _queueName;

        bool _connectionExists = false;

        private IConnection _connection;
        private IModel _channel;

        private EventingBasicConsumer _consumer;

        public EmployeeConsumerService(IOptions<RabbitMqConfiguration> rabbitMqOptions)
        {
            _uri = rabbitMqOptions.Value.Uri;
            _queueName = rabbitMqOptions.Value.QueueName;
        }

        private void ConsumerRecievedCallback(object sender, BasicDeliverEventArgs e)
        {
            byte[] body = e.Body.ToArray();
            string message = Encoding.UTF8.GetString(body);
            string[] lines = new string[] { message };
            Console.WriteLine(message);
        }

        public void Start()
        {

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Trying to establish connection...");
                _connectionExists = ConnectionExists();
                if (_connectionExists)
                {
                    Console.WriteLine("Connection established");
                    break;
                }
                else
                {
                    Console.WriteLine($"Failed to establish connection... Retry: {i + 1}");
                }
            }
            if (_connectionExists)
            {
                _channel = _connection.CreateModel();

                _channel.QueueDeclare(queue: _queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

                _consumer = new EventingBasicConsumer(_channel);
                _consumer.Received += ConsumerRecievedCallback;

                _channel.BasicConsume(_queueName, true, _consumer);
            }
            else
            {
                Console.WriteLine("Error: Failed to establish connection");
            }
        }

        public void Stop()
        {
            if (_connectionExists)
            {
                _consumer.Received -= ConsumerRecievedCallback;
                _channel.Dispose();
                _connection.Dispose();
            }
        }

        private void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    Uri = new Uri(_uri)
                };

                _connection = factory.CreateConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Couldn't create connection: {ex.Message}");
            }
        }

        private bool ConnectionExists()
        {
            if (_connection != null)
            {
                return true;
            }

            CreateConnection();

            return _connection != null;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.Run(() => Start(), cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.Run(() => Stop(), cancellationToken);
        }
    }
}
