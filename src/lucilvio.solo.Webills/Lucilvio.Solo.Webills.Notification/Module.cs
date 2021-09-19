using System;
using System.Linq;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.EventBus;

namespace Lucilvio.Solo.Webills.Notification
{
    public abstract class Module
    {
        protected readonly IEventPublisher _eventBus;
        protected readonly Configurations _configurations;

        public Module(Configurations configurations, IEventPublisher eventBus)
        {
            this._configurations = configurations ?? throw new ArgumentNullException(nameof(configurations));
            this._eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        public abstract Task HandleEvent(Event @event);

        public record Configurations
        {
            public string DataConnectionString { get; init; }
        }

        public class Error : Exception
        {
            public Error() { }
        }
    }
}