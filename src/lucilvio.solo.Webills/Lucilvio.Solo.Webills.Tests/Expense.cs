using System;

namespace Lucilvio.Solo.Webills.Tests
{
    public class Expense
    {
        private string _name;
        private DateTime _date;

        public Expense(string name, DateTime date, TransactionValue value)
        {
            this._name = name;
            this._date = date;

            if (value == null)
                throw new ExpenseTransactionValueCannotBeNull();

            this.Value = value;
        }

        public TransactionValue Value { get; }
    }
}