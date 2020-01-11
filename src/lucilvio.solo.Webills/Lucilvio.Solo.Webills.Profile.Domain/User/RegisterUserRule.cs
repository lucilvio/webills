using Lucilvio.Solo.Webills.Domain.Shared;
using Lucilvio.Solo.Webills.Profile.Domain.User.BusinessErrors;

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
