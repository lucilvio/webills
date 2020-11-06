using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.EventBus;
using Lucilvio.Solo.Webills.UserAccount.CreateAccount;
using Lucilvio.Solo.Webills.UserAccount.CreateUserAccount;
using Lucilvio.Solo.Webills.UserAccount.GenerateNewPassword;
using Lucilvio.Solo.Webills.UserAccount.Infraestructure;
using Lucilvio.Solo.Webills.UserAccount.Login;

namespace Lucilvio.Solo.Webills.UserAccount
{
    public class Module
    {
        private readonly IEventBus _eventBus;
        private readonly IMessageDispatcher _messageDispatcher;

        public Module(IEventBus eventBus, Configurations config)
        {
            this._eventBus = eventBus;
            this._messageDispatcher = new DefaultMessageDispatcher(config, this._eventBus);
        }

        public async Task<GeneratedPassword> GenerateNewPassword(IGenerateNewPasswordMessage message)
        {
            return await this._messageDispatcher.DispatchGenerateNewPasswordMessage(message);
        }

        public async Task<CreatedAccount> CreateNewAccount(ICreateNewAccountMessage message)
        {
            return await this._messageDispatcher.DispatchCreateNewAccountMessage(message);
        }

        public async Task<LoggedUser> Login(ILoginMessage message)
        {
            return await this._messageDispatcher.DispatchLoginMessage(message);
        }
    }
}