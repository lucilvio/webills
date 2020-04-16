using System;

namespace Lucilvio.Solo.Webills.Transactions.GetExpensesByFilter
{
    public class GetExpensesByFilterInput
    {
        public GetExpensesByFilterInput(Guid userId)
        {
            this.UserId = userId;
        }

        internal Guid UserId { get; }
    }
}