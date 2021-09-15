using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.EventBus;
using Lucilvio.Solo.Webills.UserAccount.Infrastructure;

namespace Lucilvio.Solo.Webills.UserAccount
{
    public abstract class Module
    {
        protected readonly Configurations _configurations;
        protected readonly IEventBus _eventBus;

        public Module(Configurations configurations, IEventBus eventBus)
        {
            this._configurations = configurations ?? throw new ArgumentNullException(nameof(configurations));
            this._eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        public abstract Task SendMessage(Message message);

        public record Configurations
        {
            public string DataConnectionString { get; init; }
            public DefaultUserAccount DefaultAccount { get; init; }
            public bool IsDefaultUserAccountConfigured => this.DefaultAccount != null;

            public record DefaultUserAccount
            {
                public string Name { get; init; }
                public string Email { get; init; }
                public string Password { get; init; }
            }
        }

        public class Error : Exception
        {
            public Error() { }
            public Error(string message) : base(message) { }
        }
    }
}