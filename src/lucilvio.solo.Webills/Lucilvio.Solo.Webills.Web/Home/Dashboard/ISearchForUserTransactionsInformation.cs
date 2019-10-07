using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lucilvio.Solo.Webills.Web.Home
{
    public interface ISearchForUserTransactionsInformation
    {
        SearchForUserTransactionsInformationResult Execute();
    }

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

            return new SearchForUserTransactionsInformationResult(user.Balance,
                user.Incomes.Select(i => new UserIncomeData(i.Name, i.Date, i.Value)),
                user.Expenses.Select(e => new UserExpenseData(e.Name, e.Date, e.Value)));
        }
    }

}