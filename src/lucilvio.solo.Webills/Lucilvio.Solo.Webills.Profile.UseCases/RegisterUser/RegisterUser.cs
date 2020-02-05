using System.Threading.Tasks;
using Lucilvio.Solo.Webills.Profile.Domain.User;
using Lucilvio.Solo.Webills.Profile.UseCases.Contracts.RegisterUser;
using Lucilvio.Solo.Webills.Shared.Domain;
using Lucilvio.Solo.Webills.Shared.UseCases.Errors;

namespace Lucilvio.Solo.Webills.Profile.UseCases.RegisterUser
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

            var userWithTheSameLogin = await this._dataStorage.GetUserByLogin(command.Login);

            var registerUserRule = new RegisterUserRule();
            registerUserRule.Verify(command.Password, command.PasswordConfirmation, userWithTheSameLogin);
            
            var newUser = new User(command.Name, command.Login, command.Password, command.TermsAccepted);

            await this._dataStorage.Persist(newUser);
        }
    }
}