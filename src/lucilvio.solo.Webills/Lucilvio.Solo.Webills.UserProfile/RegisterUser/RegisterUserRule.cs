using Lucilvio.Solo.Webills.UserProfile.Domain;
using Lucilvio.Solo.Webills.UserProfile.Domain.BusincessErrors;

namespace Lucilvio.Solo.Webills.UserProfile.RegisterUser
{
    class RegisterUserRule
    {
        internal void Verify(Password password, Password passwordConfirmation, User userWithTheSameLogin)
        {
            if (password != passwordConfirmation)
                throw new PasswordAreNotTheSame();

            if (userWithTheSameLogin != null)
                throw new LoginAlreadyInUse();
        }
    }
}
