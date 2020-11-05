using System.Threading.Tasks;
using Lucilvio.Solo.Webills.UserAccount.Login;
using Lucilvio.Solo.Webills.UserAccount.Infraestructure.DataAccess;
using Lucilvio.Solo.Webills.UserAccount.ForgotYourPassword;
using Lucilvio.Solo.Webills.EventBus;
using Lucilvio.Solo.Webills.UserAccount.CreateUserAccount;
using Lucilvio.Solo.Webills.UserAccount.CreateAccount;

namespace Lucilvio.Solo.Webills.UserAccount.Infraestructure
{
    internal class DefaultMessageDispatcher : IMessageDispatcher
    {
        private readonly IEventBus _eventBus;
        private readonly Configurations _configuration;

        public DefaultMessageDispatcher(Configurations configuration, IEventBus eventBus)
        {
            this._configuration = configuration;
            this._eventBus = eventBus;
        }

        public async Task<CreatedAccount> DispatchCreateNewAccountMessage(ICreateAccountMessage message)
        {
            using var ctx = new DataContext(this._configuration);

            ICreateAccountDataAccess dataAccess = new CreateAccountDataAccess(ctx);
            var handler = new CreateAccountMessageHandler(dataAccess, this._eventBus);

            return await handler.Execute(message);
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
