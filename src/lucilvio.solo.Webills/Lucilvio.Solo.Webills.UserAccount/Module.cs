using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.UserAccount.Login;
using Lucilvio.Solo.Webills.UserAccount.Infraestructure;
using Lucilvio.Solo.Webills.UserAccount.ForgotYourPassword;

namespace Lucilvio.Solo.Webills.UserAccount
{
    public partial class Module
    {
        private readonly IEventBus _eventBus;
        private readonly IMessageDispatcher _messageDispatcher;

        public Module(Configurations config)
        {
            this._eventBus = new DefaultEventBus();
            _messageDispatcher = new DefaultMessageDispatcher(config, this._eventBus);
        }

        public async Task<LoggedUser> Login(ILoginMessage message)
        {
            return await this._messageDispatcher.DispatchLoginMessage(message);
        }

        public async Task<GeneratedPassword> ForgotYourPassword(IForgotYourPasswordMessage message)
        {
            return await this._messageDispatcher.DispatchForgotYourPasswordMessage(message);
        }

        public void SubscribeEvent(Events @event, Func<object, Task> reaction) =>
            this._eventBus.Subscibe(@event, reaction);
    }
}