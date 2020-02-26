using System;
using Lucilvio.Solo.Webills.Dashboard.AddExpense;
using Lucilvio.Solo.Webills.Transactions.AddNewExpense;

namespace Lucilvio.Solo.Webills.Web.Home
{
    internal class AddedExpenseInputAdapter : IAddExpenseInput
    {
        private NewAddedExpense _addedExpense;

        public AddedExpenseInputAdapter(NewAddedExpense addedExpense)
        {
            this._addedExpense = addedExpense;
        }

        public Guid UserId => this._addedExpense.UserId;

        public Guid TransactionId => this._addedExpense.Id;

        public string Name => this._addedExpense.Name;

        public DateTime Date => this._addedExpense.Date;

        public decimal Value => this._addedExpense.Value;

        public int Category => this._addedExpense.Category;

        public string CategoryName => this._addedExpense.CategoryName;
    }
}