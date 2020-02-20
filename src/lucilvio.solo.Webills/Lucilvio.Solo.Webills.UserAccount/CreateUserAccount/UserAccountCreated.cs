using System;

namespace Lucilvio.Solo.Webills.UserAccount.CreateUserAccount
{
    public class UserAccountCreated
    {
        public UserAccountCreated(Guid userId)
        {
            this.Id = userId;
        }

        public Guid Id { get; }
    }
}