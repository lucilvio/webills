using System.Collections.Generic;
using System.Linq;
using Lucilvio.Solo.Webills.Clients.Web.Shared.DataFormaters;
using Lucilvio.Solo.Webills.Transactions.GetExpensesByFilter;

namespace Lucilvio.Solo.Webills.Clients.Web.Expenses
{
    public class ExpensesResponse
    {
        private ExpensesResponse()
        {
            this.Expenses = new List<ExpenseFromList>();
        }

        public ExpensesResponse(GetExpensesByFilterOutput output) : this()
        {
            if (output != null)
                this.Expenses = output.Expenses.Select(i => new ExpenseFromList(i));
        }

        public IEnumerable<ExpenseFromList> Expenses { get; set; }

        public class ExpenseFromList
        {
            public ExpenseFromList(GetExpensesByFilterOutput.Expense expense)
            {
                if (expense == null)
                    return;

                this.Id = expense.Id.ToString();
                this.Name = expense.Name;
                this.Date = expense.Date.ToDateString();
                this.Value = expense.Value.DecimalToMoney();
                this.Category = expense.CategoryName;
            }

            public string Id { get; }
            public string Name { get; }
            public string Date { get; }
            public string Value { get; }
            public string Category { get; }
        }
    }
}
