using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lucilvio.Solo.Architecture;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Lucilvio.Solo.Webills.EventBus.RabbitMq.Host
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IEnumerable<IEventListener> _listeners;
        private readonly IConnectionFactory _connectionFactory;

        public Worker(ILogger<Worker> logger, IEnumerable<IEventListener> listeners)
        {
            this._logger = logger;
            this._listeners = listeners;
            this._connectionFactory = new ConnectionFactory()
            {
                HostName = "localhost"
            };
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var connection = this._connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare("bus", true, false, false);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                var @event = JsonConvert.DeserializeObject<Event>(message);

                foreach (var listener in this._listeners)
                {
                    await listener.ListenEvent(@event);
                }

                channel.BasicAck(ea.DeliveryTag, false);
            };

            channel.BasicConsume("bus", false, consumer);


            while (!stoppingToken.IsCancellationRequested) { }

            return Task.CompletedTask;
        }
    }
}
