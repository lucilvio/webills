using System.Collections.Generic;
using System.Linq;

namespace Lucilvio.Solo.Webills.Clients.Web.Expenses.Index
{
    public class ExpensesResponse
    {
        private ExpensesResponse()
        {
            this.Expenses = new List<ExpenseFromList>();
        }

        public ExpensesResponse(GetUserExpensesByFilterQueryResult result) : this()
        {
            if (result.HasExpenses)
                this.Expenses = result.Expenses.Select(i => new ExpenseFromList(i));
        }

        public IEnumerable<ExpenseFromList> Expenses { get; set; }

        public class ExpenseFromList
        {
            public ExpenseFromList(UserExpenseData expense)
            {
                if (expense == null)
                    return;

                //this.Id = expense.Id.ToString();
                //this.Name = expense.Name;
                //this.Date = expense.Date.ToDateString();
                //this.Value = expense.Value.DecimalToMoney();
                //this.Category = expense.Category;
            }

            public string Id { get; }
            public string Name { get; }
            public string Date { get; }
            public string Value { get; }
            public int Category { get; }
        }
    }
}
