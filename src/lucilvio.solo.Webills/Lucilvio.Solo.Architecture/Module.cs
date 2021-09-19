using System;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Architecture
{
    public abstract class Module<TConfigurations>
    {
        protected readonly TConfigurations _configurations;
        protected readonly IEventPublisher _eventBus;

        public Module(TConfigurations configurations = default, IEventPublisher eventBus = null)
        {
            this._configurations = configurations;
            this._eventBus = eventBus;
        }

        public virtual Task SendMessage(Message message) => Task.CompletedTask;
        public virtual Task HandleEvent(Event @event) => Task.CompletedTask;

        public class Error : Exception
        {
            public Error() { }
            public Error(string message) : base(message) { }
        }
    }
}