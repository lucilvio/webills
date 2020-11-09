using System;

namespace Lucilvio.Solo.Webills.Transactions.CreateUser
{
    internal class CreateUserMessage
    {
        public CreateUserMessage(Guid userId)
        {
            this.UserId = userId;
        }

        public Guid UserId { get; }
    }
}
