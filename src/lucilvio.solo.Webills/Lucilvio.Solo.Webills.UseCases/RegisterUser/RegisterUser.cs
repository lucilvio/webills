using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Domain.Shared;
using Lucilvio.Solo.Webills.UseCases.Common;
using Lucilvio.Solo.Webills.Domain.Profile.User;
using Lucilvio.Solo.Webills.UseCases.Contracts.RegisterUser;

namespace Lucilvio.Solo.Webills.UseCases.RegisterUser
{
    public class RegisterUser : IRegisterUser
    {
        private readonly IRegisterUserDataStorage _dataStorage;

        public RegisterUser(IRegisterUserDataStorage dataStorage)
        {
            this._dataStorage = dataStorage;
        }

        public async Task Execute(RegisterUserCommand command)
        {
            if (command.NotDefined())
                throw new CommandNotInformed();

            if (command.Password != command.PasswordConfirmation)
                throw new PasswordAreNotTheSame();

            var userWithTheSameLogin = await this._dataStorage.GetUserByLogin(command.Login);

            if (userWithTheSameLogin.IsDefined())
                throw new LoginAlreadyInUse();

            var newUser = new User(command.Name, command.Login, command.Password, command.TermsAccepted);

            await this._dataStorage.Persist(newUser);
        }
    }
}