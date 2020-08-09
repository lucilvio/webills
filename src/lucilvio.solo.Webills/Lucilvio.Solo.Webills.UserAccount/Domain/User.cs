using System;

namespace Lucilvio.Solo.Webills.UserAccount.Domain
{
    internal class User
    {
        private User()
        {
            this.Id = Guid.NewGuid();
        }

        public User(Name name, Email email) : this()
        {
            this.Name = name ?? throw new Error.CantCreateUserWithoutName();
            this.Email = email ?? throw new Error.CantCreateUserWithoutEmail();
        }

        public Guid Id { get; }
        public Name Name { get; }
        public Email Email { get; }
        public Account Account { get; set; }

        private bool HasAccount => this.Account != null;

        public void CreateAccount(Login login, IPassword password, IPassword passwordConfirmation, bool termAccepted,
            User accountWithSameLogin)
        {
            if (accountWithSameLogin != null && accountWithSameLogin.HasAccount && accountWithSameLogin.Account.Login == login)
                throw new Error.LoginNotAvailable();

            this.Account = new Account(login, password, passwordConfirmation, termAccepted);
        }

        internal void Login(IPassword password)
        {
            if (!this.HasAccount)
                throw new Error.UserDoesntHaveAnAccountAssociated();

            this.Account.VerifyLoginPermission(password);
        }

        internal void ChangePassword(IPassword newPassword)
        {
            if (!this.HasAccount)
                throw new Error.UserDoesntHaveAnAccountAssociated();

            this.Account.ChangePassword(newPassword);
        }

        internal class Error
        {
            public class CantCreateUserWithoutName : Exception { }
            public class CantCreateUserWithoutEmail : Exception { }
            public class LoginNotAvailable : Exception { }
            public class UserDoesntHaveAnAccountAssociated : Exception { }
        }
    }
}