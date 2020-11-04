using System.Threading.Tasks;
using Lucilvio.Solo.Webills.UserAccount.Login;
using Lucilvio.Solo.Webills.UserAccount.Infraestructure.DataAccess;
using Lucilvio.Solo.Webills.UserAccount.ForgotYourPassword;

namespace Lucilvio.Solo.Webills.UserAccount.Infraestructure
{
    internal class DefaultMessageDispatcher : IMessageDispatcher
    {
        private readonly Configurations _configuration;
        private readonly IEventBus _eventBus;

        public DefaultMessageDispatcher(Configurations configuration, IEventBus eventBus)
        {
            this._configuration = configuration;
            this._eventBus = eventBus;
        }

        public async Task<GeneratedPassword> DispatchForgotYourPasswordMessage(IForgotYourPasswordMessage message)
        {
            using var ctx = new DataContext(this._configuration);

            IForgotYourPasswordDataAccess dataAccess = new ForgotYourPasswordDataAccess(ctx);
            var handler = new ForgotYourPasswordMessageHandler(dataAccess, this._eventBus);

            return await await handler.Execute(message);
        }

        public async Task<LoggedUser> DispatchLoginMessage(ILoginMessage message)
        {
            using var ctx = new DataContext(this._configuration);
            
            ILoginDataAccess dataAccess = new LoginDataAccess(ctx);
            var handler = new LoginMessageHandler(dataAccess, this._eventBus);

            return await handler.Execute(message);
        }
    }
}
