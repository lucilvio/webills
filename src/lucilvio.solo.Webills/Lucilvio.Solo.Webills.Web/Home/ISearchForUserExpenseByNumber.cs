using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Lucilvio.Solo.Webills.Web.Home
{
    public interface ISearchForUserExpenseByNumber
    {
        Task<SearchForUserExpenseByNumberResult> Execute(SearchForUserExpenseByNumberQuery query);
    }

    public class SearchForUserExpenseByNumber : ISearchForUserExpenseByNumber
    {
        private readonly WebillsContext _context;

        public SearchForUserExpenseByNumber(WebillsContext context)
        {
            this._context = context;
        }

        public async Task<SearchForUserExpenseByNumberResult> Execute(SearchForUserExpenseByNumberQuery query)
        {
            var foundUser = await this._context.Users.AsNoTracking().Include(u => u.Expenses).FirstOrDefaultAsync();
            var foundIncome = foundUser.Expenses.FirstOrDefault(e => e.Number == query.Number);

            return new SearchForUserExpenseByNumberResult(foundIncome);
        }
    }
}