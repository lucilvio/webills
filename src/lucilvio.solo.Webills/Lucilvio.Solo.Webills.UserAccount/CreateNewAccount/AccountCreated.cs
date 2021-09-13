using System;
using Lucilvio.Solo.Webills.UserAccount.Domain;

namespace Lucilvio.Solo.Webills.UserAccount.CreateNewAccount
{
    public class AccountCreated
    {
        internal AccountCreated(User user)
        {
            this.UserId = user != null ? user.Id : Guid.Empty;
        }

        public Guid UserId { get; }
    }
}