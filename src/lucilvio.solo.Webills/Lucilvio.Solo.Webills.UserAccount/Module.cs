using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.UserAccount.Login;

namespace Lucilvio.Solo.Webills.UserAccount
{
    public class Module
    {
        private readonly IEventBus _eventBus;
        private readonly IMessageResolver _messageResolver;

        public Module(Configurations configs = null)
        {
            this._eventBus = new DefaultEventBus();
            this._messageResolver = new DefaultMessageResolver(this._eventBus, configs);
        }

        public async Task SendMessage(Messages message, object input) =>
            await this._messageResolver.Resolve(message, input);

        public void SubscribeEvent(Events @event, Func<object, Task> reaction) =>
            this._eventBus.Subscibe(@event, reaction);

        public enum Messages
        {
            Login,
            NewPassword,
            CreateAccount
        }

        public enum Events
        {
            OnLogin,
            OnAccountCreated
        }

        public class Configurations
        {
            public string DataConnection { get; set; }
        }
    }
}