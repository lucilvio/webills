using System;

using Lucilvio.Solo.Webills.Profile.Domain.User.BusinessErrors;

namespace Lucilvio.Solo.Webills.Profile.Domain.User
{
    public class User
    {
        private User()
        {
            this.Id = Guid.NewGuid();
        }

        public User(string name, Login login, Password password, bool termsAccepted) : this()
        {
            this.Name = name;
            this.Login = login;
            this.Password = password;

            if (!termsAccepted)
                throw new IsNecessaryToAcceptTheTerms();

            this.TermsAccepted = termsAccepted;
        }

        public Guid Id { get; }
        public string Name { get; }
        public Login Login { get; }
        public Password Password { get; }
        public bool TermsAccepted { get; }
    }
}
