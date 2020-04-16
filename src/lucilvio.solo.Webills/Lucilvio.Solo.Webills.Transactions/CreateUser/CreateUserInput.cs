using System;

namespace Lucilvio.Solo.Webills.Transactions.CreateUser
{
    public class CreateUserInput
    {
        public CreateUserInput(Guid id)
        {
            this.Id = id;
        }

        internal Guid Id { get; }
    }
}