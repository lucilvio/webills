using System.Threading.Tasks;
using Lucilvio.Solo.Webills.UserAccount.Login;
using Lucilvio.Solo.Webills.UserAccount.Infraestructure.DataAccess;
using Lucilvio.Solo.Webills.EventBus;
using Lucilvio.Solo.Webills.UserAccount.CreateUserAccount;
using Lucilvio.Solo.Webills.UserAccount.CreateAccount;
using Lucilvio.Solo.Webills.UserAccount.CreateNewAccount;
using Lucilvio.Solo.Webills.UserAccount.GenerateNewPassword;

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

        public async Task<CreatedAccount> DispatchCreateNewAccountMessage(ICreateNewAccountMessage message)
        {
            using var ctx = new DataContext(this._configuration);

            ICreateNewAccountDataAccess dataAccess = new CreateNewAccountDataAccess(ctx);
            var handler = new CreateNewAccountMessageHandler(dataAccess, this._eventBus);

            return await handler.Execute(message);
        }

        public async Task<GeneratedPassword> DispatchGenerateNewPasswordMessage(IGenerateNewPasswordMessage message)
        {
            using var ctx = new DataContext(this._configuration);

            IGenerateNewPasswordDataAccess dataAccess = new GenerateNewPassword.GenerateNewPasswordDataAccess(ctx);
            var handler = new GenerateNewPasswordMessageHandler(dataAccess, this._eventBus);

            return await handler.Execute(message);
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
