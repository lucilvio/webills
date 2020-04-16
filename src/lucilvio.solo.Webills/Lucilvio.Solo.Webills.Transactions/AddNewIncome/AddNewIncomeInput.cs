using System;

namespace Lucilvio.Solo.Webills.Transactions.AddNewIncome
{
    public class AddNewIncomeInput
    {
        public AddNewIncomeInput(Guid userId, string name, DateTime date, decimal value)
        {
            this.UserId = userId;
            this.Name = name;
            this.Date = date;
            this.Value = value;
        }

        internal Guid UserId { get; }
        internal string Name { get; }
        internal DateTime Date { get; }
        internal decimal Value { get; }
    }
}