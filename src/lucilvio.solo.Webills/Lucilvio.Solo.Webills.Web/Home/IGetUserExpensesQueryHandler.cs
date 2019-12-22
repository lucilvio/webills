using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Lucilvio.Solo.Webills.Web.Home
{
    public interface IGetUserExpensesQueryHandler
    {
        Task<GetUserExpensesQueryResult> Execute(GetUserExpensesByNumberQuery query);
    }

    public class GetUserExpensesQueryHandler : IGetUserExpensesQueryHandler
    {
        private readonly WebillsContext _context;

        public GetUserExpensesQueryHandler(WebillsContext context)
        {
            this._context = context;
        }

        public async Task<GetUserExpensesQueryResult> Execute(GetUserExpensesByNumberQuery query)
        {
            var foundUser = await this._context.Users.AsNoTracking().Include(u => u.Expenses).FirstOrDefaultAsync();
            var foundIncome = foundUser.Expenses.FirstOrDefault(e => e.Number == query.Number);

            return new GetUserExpensesQueryResult(foundIncome);
        }
    }
}