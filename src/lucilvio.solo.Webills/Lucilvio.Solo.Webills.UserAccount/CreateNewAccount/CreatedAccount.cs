using System;
using Lucilvio.Solo.Webills.UserAccount.Domain;

namespace Lucilvio.Solo.Webills.UserAccount.CreateNewAccount
{
    internal class CreatedAccount
    {
        public CreatedAccount(User user)
        {
            if (user is null)
                return;

            this.UserName = user.Name.Value;
        }

        public string UserName { get; }
    }
}