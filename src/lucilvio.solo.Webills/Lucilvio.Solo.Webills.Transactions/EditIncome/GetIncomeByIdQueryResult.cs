using System;

namespace Lucilvio.Solo.Webills.Transactions.GetIncomeById
{
    public class GetIncomeByIdQueryResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
    }
}