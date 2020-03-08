using System;

using Lucilvio.Solo.Webills.UserProfile.Domain;

namespace Lucilvio.Solo.Webills.UserAccount.Domain
{
    internal class User
    {
        private User()
        {
            Id = Guid.NewGuid();
        }

        public User(Name name, Login login, IPassword password, bool termsAccepted) : this()
        {
            this.Name = name ?? throw new Error.CannotCreateUserWithoutName();
            this.Login = login ?? throw new Error.CannotCreateUserWithoutLogin();
            this.Password = password ?? throw new Error.CannotCreateUserWithoutPassword(); ;
            this.TermAccepted = termsAccepted;
        }

        internal void ChangePassword(IPassword newPassword)
        {
            this.Password = newPassword;
        }

        public Guid Id { get; }
        public Name Name { get; }
        public Login Login { get; }
        public bool TermAccepted { get; }
        public IPassword Password { get; private set; }

        internal class Error
        {
            public class CannotCreateUserWithoutName : Exception { }
            public class CannotCreateUserWithoutLogin : Exception { }
            public class CannotCreateUserWithoutPassword : Exception { }
        }
    }
}