using System;
using Lucilvio.Solo.Webills.Clients.Web.Shared.DataFormaters;
using Lucilvio.Solo.Webills.Transactions.GetExpense;

namespace Lucilvio.Solo.Webills.Clients.Web.Expenses
{
    public class GetExpenseResponse
    {
        public GetExpenseResponse(GetExpenseByIdOutput expense)
        {
            if (expense == null)
                return;

            this.Id = expense.Id;
            this.Name = expense.Name;
            this.Date = expense.Date.ToDateString();
            this.Category = expense.Category;
            this.Value = expense.Value.DecimalToMoney();
        }

        public Guid Id { get; }
        public string Name { get; }
        public string Date { get; }
        public int Category { get; }
        public string Value { get; }
    }
}