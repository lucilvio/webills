using System;

namespace Lucilvio.Solo.Webills.FinancialControl.GetExpensesByFilter
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