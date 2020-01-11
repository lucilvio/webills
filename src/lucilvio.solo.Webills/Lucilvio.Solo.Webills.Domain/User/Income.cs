using System;
using Lucilvio.Solo.Webills.Domain.User.BusinessErrors;

namespace Lucilvio.Solo.Webills.Domain.User
{
    public class Income
    {
        private Income()
        {
            this.Id = Guid.NewGuid();
        }

        internal Income(string name, DateTime date, TransactionValue value) : this()
        {
            if (string.IsNullOrEmpty(name))
                throw new IncomeMustHaveName();

            this.Name = name;
            this.Date = date;

            if (value == null)
                throw new IncomeTransactionValueCannotBeNull();

            this.Value = value;
        }

        public Guid Id { get; }
        public string Name { get; }
        public DateTime Date { get; }
        public TransactionValue Value { get; }
    }
}