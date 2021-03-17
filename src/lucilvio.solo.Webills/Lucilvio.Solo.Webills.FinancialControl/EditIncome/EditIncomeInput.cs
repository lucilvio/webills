using System;

namespace Lucilvio.Solo.Webills.FinancialControl.EditIncome
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

        internal Guid UserId { get; }
        internal Guid Id { get; }
        internal string Name { get; }
        internal DateTime Date { get; }
        internal decimal Value { get; }
    }
}