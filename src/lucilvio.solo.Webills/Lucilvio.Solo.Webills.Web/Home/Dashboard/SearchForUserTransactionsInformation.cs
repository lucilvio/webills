using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lucilvio.Solo.Webills.Web.Home
{
    public class SearchForUserTransactionsInformation : ISearchForUserTransactionsInformation
    {
        private readonly WebillsContext _context;

        public SearchForUserTransactionsInformation(WebillsContext context)
        {
            this._context = context;
        }

        public SearchForUserTransactionsInformationResult Execute()
        {
            var user = this._context.Users.Include(u => u.Incomes).Include(u => u.Expenses).AsNoTracking().FirstOrDefault();

            if (user == null)
                return SearchForUserTransactionsInformationResult.Empty;

            return new SearchForUserTransactionsInformationResult(
                user.Balance,
                user.TotalIncomes,
                user.TotalExpenses,
                user.Incomes.Select(i => new UserIncomeData(i.Number, i.Name, i.Date, i.Value)),
                user.Expenses.Select(e => new UserExpenseData(e.Number, e.Name, e.Category, e.Date, e.Value)));
        }
    }

}