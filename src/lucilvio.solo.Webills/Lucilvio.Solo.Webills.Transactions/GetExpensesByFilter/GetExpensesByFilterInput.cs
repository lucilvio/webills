using System;

namespace Lucilvio.Solo.Webills.Transactions.GetExpensesByFilter
{
    public class GetExpensesByFilterInput
    {
        public GetExpensesByFilterInput(Guid userId)
        {
            this.UserId = userId;
        }

        public Guid UserId { get; }
    }
}