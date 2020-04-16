using System;

namespace Lucilvio.Solo.Webills.Dashboard.RemoveTransaction
{
    public class RemoveTransactionInput
    {
        public RemoveTransactionInput(Guid userId, Guid id)
        {
            this.Id = id;
            this.UserId = userId;
        }

        internal Guid Id { get; }
        internal Guid UserId { get; }
    }
}
