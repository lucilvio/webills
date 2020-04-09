using System;

namespace Lucilvio.Solo.Webills.Transactions.GetExpense
{
    public class GetExpenseByIdInput
    {
        public GetExpenseByIdInput(Guid userId, Guid id)
        {
            this.UserId = userId;
            this.Id = id;
        }

        public Guid UserId { get; }
        public Guid Id { get; }
    }
}