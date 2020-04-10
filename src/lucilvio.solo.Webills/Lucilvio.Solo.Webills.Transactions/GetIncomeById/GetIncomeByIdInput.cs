using System;

namespace Lucilvio.Solo.Webills.Transactions.GetIncomeById
{
    public class GetIncomeByIdInput
    {
        public GetIncomeByIdInput(Guid userId, Guid id)
        {
            this.UserId = userId;
            this.Id = id;
        }

        public Guid UserId { get; }
        public Guid Id { get; }
    }
}