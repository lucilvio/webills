using System.Text;
using System.Threading.Tasks;
using Lucilvio.Solo.Architecture;
using RabbitMQ.Client;

namespace Lucilvio.Solo.Webills.EventBus.RabbitMq
{
    internal class RabbitMqEventPublisher : IEventPublisher
    {
        private readonly ConnectionFactory _connectionFactory;

        public RabbitMqEventPublisher(Configurations configurations)
        {
            this._connectionFactory = new ConnectionFactory
            {
                HostName = configurations.Host
            };
        }

        public async Task Publish(Event @event)
        {
            using var connection = this._connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.BasicPublish("", "bus", body: Encoding.UTF8.GetBytes(@event.Serialize()));
        }
    }

    public record Configurations
    {
        public string Host { get; set; }
    }
}