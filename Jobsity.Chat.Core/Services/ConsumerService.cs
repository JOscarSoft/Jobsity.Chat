using System;
using System.Text.Json;
using Jobsity.Chat.Core.Contracts;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;


namespace Jobsity.Chat.Core.Services
{
    public class ConsumerService : IConsumerService, IDisposable
    {
        private IConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _modelChannel;
        public ConsumerService(string connectionString)
        {
            _connectionFactory = new ConnectionFactory
            {
                Uri = new Uri(connectionString)
            };
        }

        public void RabbitConsume<T>(string queue, Action<T> execute)
        {
            _connection = _connectionFactory.CreateConnection();
            _modelChannel = _connection.CreateModel();
            _modelChannel.QueueDeclare(queue,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            EventingBasicConsumer bConsumer = new EventingBasicConsumer(_modelChannel);
            bConsumer.Received += (sender, e) =>
            {
                byte[] body = e.Body.ToArray();
                T queueObject = JsonSerializer.Deserialize<T>(body);
                execute(queueObject);
            };

            bConsumer.Registered += OnConsumerRegistered;
            bConsumer.Shutdown += OnConsumerShutdown;
            bConsumer.Unregistered += OnConsumerUnregistered;
            bConsumer.ConsumerCancelled += OnConsumerCanceled;
            _modelChannel.BasicConsume(queue, true, bConsumer);
        }

        public void Dispose()
        {
            _modelChannel.Close();
            _connection.Close();
        }

        private void OnConsumerCanceled(object sender, ConsumerEventArgs e) { }
        private void OnConsumerRegistered(object sender, ConsumerEventArgs e) { }
        private void OnConsumerShutdown(object sender, ShutdownEventArgs e) { }
        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e) { }

    }
}