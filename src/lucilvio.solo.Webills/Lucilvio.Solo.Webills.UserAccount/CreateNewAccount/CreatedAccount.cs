using System;
using Lucilvio.Solo.Webills.UserAccount.Domain;

namespace Lucilvio.Solo.Webills.UserAccount.CreateUserAccount
{
    public class CreatedAccount
    {
        internal CreatedAccount(User user)
        {
            this.Id = user != null ? user.Id : Guid.Empty;
        }

        public Guid Id { get; }
    }
}