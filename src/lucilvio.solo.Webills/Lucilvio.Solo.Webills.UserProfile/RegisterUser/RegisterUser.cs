using System.Threading.Tasks;

using Lucilvio.Solo.Webills.UserProfile.Domain;

namespace Lucilvio.Solo.Webills.UserProfile.RegisterUser
{
    class RegisterUser : IRegisterUser
    {
        private readonly IRegisterUserDataStorage _dataStorage;

        internal RegisterUser(IRegisterUserDataStorage dataStorage)
        {
            _dataStorage = dataStorage;
        }

        public async Task Register(RegisterUserCommand command)
        {
            if (command == null)
                throw new CommandNotInformed();

            var userWithTheSameLogin = await _dataStorage.GetUserByLogin(command.Login);

            var registerUserRule = new RegisterUserRule();
            registerUserRule.Verify(command.Password, command.PasswordConfirmation, userWithTheSameLogin);

            var newUser = new User(command.Name, command.Login, new Sha1EncryptedPassword(command.Password), command.TermsAccepted);

            await _dataStorage.Persist(newUser);
        }
    }
}