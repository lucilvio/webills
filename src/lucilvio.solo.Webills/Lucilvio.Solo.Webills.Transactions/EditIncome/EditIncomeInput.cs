using System;

namespace Lucilvio.Solo.Webills.Transactions.EditIncome
{
    public class EditIncomeInput
    {
        public EditIncomeInput(Guid userId, Guid id, string name, DateTime date, decimal value)
        {
            this.UserId = userId;
            this.Id = id;
            this.Name = name;
            this.Date = date;
            this.Value = value;
        }

        public Guid UserId { get; }
        public Guid Id { get; }
        public string Name { get; }
        public DateTime Date { get; }
        public decimal Value { get; }
    }
}