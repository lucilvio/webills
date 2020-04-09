using System;

namespace Lucilvio.Solo.Webills.Transactions.GetIncomesByFilter
{
    public class GetIncomesByFilterInput
    {
        public GetIncomesByFilterInput(Guid userId)
        {
            this.UserId = userId;
        }

        public Guid UserId { get; }
    }
}