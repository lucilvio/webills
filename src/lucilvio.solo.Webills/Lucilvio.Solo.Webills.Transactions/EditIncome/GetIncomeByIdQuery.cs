using System;

namespace Lucilvio.Solo.Webills.Transactions.GetIncomeById
{
    public class GetIncomeByIdQuery : IQuery
    {
        public GetIncomeByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
