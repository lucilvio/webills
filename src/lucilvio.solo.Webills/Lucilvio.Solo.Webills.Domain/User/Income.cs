using System;
using Lucilvio.Solo.Webills.Domain.User.BusinessErrors;

namespace Lucilvio.Solo.Webills.Domain.User
{
    public class Income
    {
        internal Income(string name, DateTime date, TransactionValue value)
        {
            if (string.IsNullOrEmpty(name))
                throw new IncomeMustHaveName();

            this.Name = name;
            this.Date = date;

            if (value == null)
                throw new IncomeTransactionValueCannotBeNull();

            this.Value = value;

            this.Number = Guid.NewGuid();
        }

        public Guid Number { get; }
        public string Name { get; }
        public DateTime Date { get; }
        public TransactionValue Value { get; }
    }
}