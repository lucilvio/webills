using System;

using Lucilvio.Solo.Webills.UserProfile.Domain.BusincessErrors;

namespace Lucilvio.Solo.Webills.UserProfile.Domain
{
    public class User
    {
        private User()
        {
            Id = Guid.NewGuid();
        }

        public User(string name, Login login, IPassword password, bool termsAccepted) : this()
        {
            if (string.IsNullOrEmpty(name))
                throw new NameIsRequired();

            Name = name;
            Login = login;
            Password = password;
            
            if (!termsAccepted)
                throw new IsNecessaryToAcceptTheTerms();

            TermsAccepted = termsAccepted;
        }

        public Guid Id { get; }
        public string Name { get; }
        public Login Login { get; }
        public IPassword Password { get; }
        public bool TermsAccepted { get; }
    }
}
