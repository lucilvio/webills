using System;

namespace Lucilvio.Solo.Webills.Transactions.GetIncomesByFilter
{
    public class GetIncomesByFilterInput
    {
        public GetIncomesByFilterInput(Guid userId)
        {
            this.UserId = userId;
        }

        internal Guid UserId { get; }
    }
}