using System.Linq;
using System.Collections.Generic;

using Lucilvio.Solo.Webills.Web;
using Lucilvio.Solo.Webills.Web.Shared;
using Lucilvio.Solo.Webills.Shared.Domain;
using Lucilvio.Solo.Webills.Core.Domain.User;
using Lucilvio.Solo.Webills.Infraestructure.DapperDataStorage;

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
                if (expense.NotDefined())
                    return;

                this.Id = expense.Id.ToString();
                this.Name = expense.Name;
                this.Date = expense.Date.ToDateString();
                this.Value = expense.Value.DecimalToMoney();
                this.Category = ((Category)expense.Category).ToString();
            }

            public string Id { get; }
            public string Name { get; }
            public string Date { get; }
            public string Value { get; }
            public string Category { get; }
        }
    }
}
