using System.Linq;
using System.Collections.Generic;

using Lucilvio.Solo.Webills.Shared.Domain;

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
            if (expenses.IsDefined())
                this.Expenses = expenses;
        }

        public IEnumerable<UserExpenseData> Expenses { get; private set; }

        public bool HasExpenses => this.Expenses.IsDefined() && this.Expenses.Any();
    }
}
