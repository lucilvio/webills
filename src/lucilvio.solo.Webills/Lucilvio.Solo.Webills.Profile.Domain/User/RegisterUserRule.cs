using Lucilvio.Solo.Webills.Profile.Domain.User.BusinessErrors;
using Lucilvio.Solo.Webills.Shared.Domain;

namespace Lucilvio.Solo.Webills.Profile.Domain.User
{
    public class RegisterUserRule
    {
        public void Verify(Password password, Password passwordConfirmation, User userWithTheSameLogin)
        {
            if (password != passwordConfirmation)
                throw new PasswordAreNotTheSame();

            if (userWithTheSameLogin.IsDefined())
                throw new LoginAlreadyInUse();
        }
    }
}
