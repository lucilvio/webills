using System;

namespace Lucilvio.Solo.Webills.Transactions.EditExpense
{
    public class GetExpenseByIdQuery : IQuery
    {
        public GetExpenseByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}