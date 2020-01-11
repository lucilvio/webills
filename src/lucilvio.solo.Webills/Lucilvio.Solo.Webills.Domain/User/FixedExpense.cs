using Lucilvio.Solo.Webills.Domain.User.BusinessErrors;
using System;

namespace Lucilvio.Solo.Webills.Domain.User
{
    public class FixedExpense : Expense
    {
        internal FixedExpense(string name, Category category, DateTime date, TransactionValue value, Recurrency recurrency, DateTime until)
            : base(name, category, date, value)
        {
            if (until < date)
                throw new FixedExpenseLimitDateMustBeGreaterThenExpenseDate();

            this.Until = until;
            this.Recurrency = recurrency;
        }

        public DateTime Until { get; set; }
        public Recurrency Recurrency { get; }
    }
}