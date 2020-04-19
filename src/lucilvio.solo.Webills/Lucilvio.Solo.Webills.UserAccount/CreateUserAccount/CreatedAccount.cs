using System;

using Lucilvio.Solo.Webills.UserAccount.Domain;

namespace Lucilvio.Solo.Webills.UserAccount.CreateUserAccount
{
    public class CreatedAccount
    {
        internal CreatedAccount(User user)
        {
            this.Id = user.Id;
        }

        public Guid Id { get; }
    }
}