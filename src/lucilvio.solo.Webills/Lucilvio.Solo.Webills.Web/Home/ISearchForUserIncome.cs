using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Lucilvio.Solo.Webills.Web.Home
{
    public interface ISearchForUserIncomeByNumber
    {
        Task<SearchForUserIncomeByNumberResult> Execute(SearchForUserIncomeByNumberQuery query);
    }

    public class SarchForUserIncomeByNumber : ISearchForUserIncomeByNumber
    {
        private readonly WebillsContext _context;

        public SarchForUserIncomeByNumber(WebillsContext context)
        {
            this._context = context;
        }

        public async Task<SearchForUserIncomeByNumberResult> Execute(SearchForUserIncomeByNumberQuery query)
        {
            var foundUser = await this._context.Users.AsNoTracking().Include(u => u.Incomes).FirstOrDefaultAsync();
            var foundIncome = foundUser.Incomes.FirstOrDefault(i => i.Number == query.Number);

            return new SearchForUserIncomeByNumberResult(foundIncome);
        }
    }
}