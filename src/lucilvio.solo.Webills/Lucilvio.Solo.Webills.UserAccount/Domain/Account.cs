using System;

namespace Lucilvio.Solo.Webills.UserAccount.Domain
{
    internal class Account
    {
        private Account()
        {
            this.Id = Guid.NewGuid();
        }

        public Account(Login login, IPassword password, IPassword passwordConfirmation, bool termAccepted) : this()
        {
            this.Login = login ?? throw new Error.CantCreateAccountWithoutLogin();
            this.Password = password ?? throw new Error.CantCreateAccountWithoutPassword();
            this.TermAccepted = termAccepted;

            if (password.Value != passwordConfirmation.Value)
                throw new Error.PasswordsDontMatch();
        }

        public Guid Id { get; }
        public Login Login { get; }
        public bool TermAccepted { get; }
        public IPassword Password { get; private set; }

        internal void ChangePassword(IPassword newPassword)
        {
            if (newPassword == null)
                throw new Error.CantChangePasswordToAnEmptyOne();

            this.Password = newPassword;
        }

        internal void VerifyLoginPermission(IPassword password)
        {
            if (this.Password.Value != password.Value)
                throw new Error.LoginOrPasswordInvalid();
        }

        internal class Error
        {
            internal class CantCreateAccountWithoutLogin : Architecture.Error { }
            internal class CantCreateAccountWithoutPassword : Architecture.Error { }
            internal class PasswordsDontMatch : Architecture.Error { }
            internal class LoginOrPasswordInvalid : Architecture.Error { }
            internal class CantChangePasswordToAnEmptyOne : Architecture.Error { }
        }

    }
}