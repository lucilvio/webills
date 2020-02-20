using System.Collections.Generic;
using System.Linq;

namespace Lucilvio.Solo.Webills.Infraestructure.DapperDataStorage
{
    public class GetUserExpensesByFilterQueryResult
    {
        private GetUserExpensesByFilterQueryResult()
        {
            this.Expenses = new List<UserExpenseData>();
        }

        public GetUserExpensesByFilterQueryResult(IEnumerable<UserExpenseData> expenses) : this()
        {
            if (expenses != null)
                this.Expenses = expenses;
        }

        public IEnumerable<UserExpenseData> Expenses { get; private set; }

        public bool HasExpenses => this.Expenses != null && this.Expenses.Any();
    }
}
