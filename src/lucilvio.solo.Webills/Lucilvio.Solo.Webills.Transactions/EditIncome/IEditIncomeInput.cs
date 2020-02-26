using System;

namespace Lucilvio.Solo.Webills.Transactions.EditIncome
{
    public interface IEditIncomeInput
    {
        public Guid UserId { get; }
        public Guid Id { get; }
        public string Name { get; }
        public DateTime Date { get; }
        public decimal Value { get; }
    }
}