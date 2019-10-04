using System.Linq;

namespace Lucilvio.Solo.Webills.Web.Home
{
    public class SearchForUserIncomesInMemory : ISearchForUserIncomes
    {
        private readonly DataStorageContext _context;

        public SearchForUserIncomesInMemory(DataStorageContext context)
        {
            this._context = context;
        }

        public SearchForUserIncomesResponse Execute()
        {
            return new SearchForUserIncomesResponse(
                this._context.Users.SelectMany(u => u.Incomes).Select(i => new UserIncomeData(i.Name, i.Date, i.Value)));
        }
    }
}