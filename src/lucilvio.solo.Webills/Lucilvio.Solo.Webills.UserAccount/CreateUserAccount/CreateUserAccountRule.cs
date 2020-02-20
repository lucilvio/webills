using System;

using Lucilvio.Solo.Webills.UserAccount.Domain;

namespace Lucilvio.Solo.Webills.UserAccount.CreateUserAccount
{
    internal class CreateUserAccountRule
    {
        internal void Verify(Password password, Password passwordConfirmation, User userWithTheSameLogin)
        {
            if (password != passwordConfirmation)
                throw new Error.PasswordsAreNotTheSame();

            if (userWithTheSameLogin != null)
                throw new Error.LoginAlreadyInUse();
        }

        internal class Error
        {
            public class PasswordsAreNotTheSame : Exception { }
            public class LoginAlreadyInUse : Exception { }
        }
    }
}
