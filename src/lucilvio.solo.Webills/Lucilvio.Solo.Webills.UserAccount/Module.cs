using System;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.UserAccount
{
    public abstract class Module
    {
        protected readonly Configurations _configurations;

        public Module(Configurations configurations)
        {
            this._configurations = configurations ?? throw new ArgumentNullException(nameof(configurations));
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