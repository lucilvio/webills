using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lucilvio.Solo.Webills.Web.Home
{
    public interface ISearchForUserIncomeByNumber
    {
        SearchForUserIncomeByNumberResult Execute(SearchForUserIncomeByNumberQuery query);
    }

    public class SarchForUserIncomeByNumber : ISearchForUserIncomeByNumber
    {
        private readonly WebillsContext _context;

        public SarchForUserIncomeByNumber(WebillsContext context)
        {
            this._context = context;
        }

        public SearchForUserIncomeByNumberResult Execute(SearchForUserIncomeByNumberQuery query)
        {
            var income = this._context.Users.AsNoTracking().Include(u => u.Incomes).FirstOrDefault().Incomes
                .FirstOrDefault(i => i.Number == query.Number);

            return new SearchForUserIncomeByNumberResult(income);
        }
    }
}