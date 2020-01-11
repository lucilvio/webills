using System;

using Lucilvio.Solo.Webills.Core.Domain.User.BusinessErrors;

namespace Lucilvio.Solo.Webills.Core.Domain.User
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