using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Architecture;

namespace Lucilvio.Solo.Webills.Notification
{
    public abstract class Module
    {
        protected readonly Configurations _configurations;

        public Module(Configurations configurations)
        {
            this._configurations = configurations ?? throw new ArgumentNullException(nameof(configurations));            
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