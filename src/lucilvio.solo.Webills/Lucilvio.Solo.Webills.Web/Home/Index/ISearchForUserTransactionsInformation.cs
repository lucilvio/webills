using System.Linq;

namespace Lucilvio.Solo.Webills.Web.Home
{
    public interface ISearchForUserTransactionsInformation
    {
        SearchForUserTransactionsInformationResult Execute();
    }

    public class SearchForUserTransactionsInformation : ISearchForUserTransactionsInformation
    {
        private readonly DataStorageContext _context;

        public SearchForUserTransactionsInformation(DataStorageContext context)
        {
            this._context = context;
        }

        public SearchForUserTransactionsInformationResult Execute()
        {
            var foundUserTransactionsInformation = this._context.Users.FirstOrDefault();

            if (foundUserTransactionsInformation == null)
                return SearchForUserTransactionsInformationResult.Empty;

            return new SearchForUserTransactionsInformationResult(foundUserTransactionsInformation.Balance,
                foundUserTransactionsInformation.Incomes.Select(i => new UserIncomeData(i.Name, i.Date, i.Value)), 
                foundUserTransactionsInformation.Expenses.Select(e => new UserExpenseData(e.Name, e.Date, e.Value)));
        }
    }

}