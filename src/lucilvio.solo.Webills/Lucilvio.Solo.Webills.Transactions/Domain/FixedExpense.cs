using System;

namespace Lucilvio.Solo.Webills.Transactions.Domain
{
    class FixedExpense : Expense
    {
        internal FixedExpense(string name, Category category, DateTime date, TransactionValue value, Recurrency recurrency, DateTime until)
            : base(name, category, date, value)
        {
            if (until < date)
                throw new Error.FixedExpenseLimitDateMustBeGreaterThenExpenseDate();

            Until = until;
            Recurrency = recurrency;
        }

        public DateTime Until { get; set; }
        public Recurrency Recurrency { get; }

        internal class Error
        {
            internal class FixedExpenseLimitDateMustBeGreaterThenExpenseDate : Exception { }
        }
    }
}