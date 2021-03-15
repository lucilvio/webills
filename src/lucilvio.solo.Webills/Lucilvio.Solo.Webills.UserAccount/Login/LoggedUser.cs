using System;
using Lucilvio.Solo.Webills.UserAccount.Domain;

namespace Lucilvio.Solo.Webills.UserAccount.Login
{
    public class LoggedUser
    {
        internal LoggedUser(User user)
        {
            if (user is null)
                return;

            this.Id = user.Id;
            this.Name = user.Name.Value;
            this.Email = user.Email.Value;
        }

        public Guid Id { get; }
        public string Name { get; }
        public string Email { get; }
    }
}
