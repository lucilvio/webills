using System;

using Lucilvio.Solo.Webills.UserAccount.Domain;

namespace Lucilvio.Solo.Webills.UserAccount.CreateUserAccount
{
    public class UserAccountCreated
    {
        internal UserAccountCreated(User user)
        {
            this.Id = user.Id;
        }

        public Guid Id { get; }
    }
}