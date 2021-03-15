using System.Threading.Tasks;
using Lucilvio.Solo.Webills.UserAccount.CreateNewAccount;
using Lucilvio.Solo.Webills.UserAccount.GenerateNewPassword;
using Lucilvio.Solo.Webills.UserAccount.Infraestructure;
using Lucilvio.Solo.Webills.UserAccount.Login;

namespace Lucilvio.Solo.Webills.UserAccount
{
    public class Module
    {
        private readonly IMessageDispatcher _messageDispatcher;
        private readonly Configurations _configurations;

        public Module(Configurations configurations)
        {
            this._messageDispatcher = new DefaultMessageDispatcher();
            this._configurations = configurations ?? throw new System.ArgumentNullException(nameof(configurations));
        }

        public async Task<GeneratedPassword> GenerateNewPassword(GenerateNewPasswordMessage message)
        {
            return await this._messageDispatcher.Dispatch(message, this._configurations);
        }

        public async Task<CreatedAccount> CreateNewAccount(CreateNewAccountMessage message)
        {
            return await this._messageDispatcher.Dispatch(message, this._configurations);
        }

        public async Task<LoggedUser> Login(LoginMessage message)
        {
            return await this._messageDispatcher.Dispatch(message, this._configurations);
        }
    }
}