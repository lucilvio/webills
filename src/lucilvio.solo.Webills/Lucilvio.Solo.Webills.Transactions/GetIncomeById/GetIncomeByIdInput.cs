using System;

namespace Lucilvio.Solo.Webills.FinancialControl.GetIncomeById
{
    public class GetIncomeByIdInput
    {
        public GetIncomeByIdInput(Guid userId, Guid id)
        {
            this.UserId = userId;
            this.Id = id;
        }

        internal Guid UserId { get; }
        internal Guid Id { get; }
    }
}