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
        private readonly WebillsCoreContext _context;

        public GetUserExpensesQueryHandler(WebillsCoreContext context)
        {
            this._context = context;
        }

        public async Task<GetUserExpensesQueryResult> Execute(GetUserExpensesByNumberQuery query)
        {
            var foundUser = await this._context.Users.AsNoTracking().Include(u => u.Expenses).FirstOrDefaultAsync();
            var foundIncome = foundUser.Expenses.FirstOrDefault(e => e.Id == query.Number);

            return new GetUserExpensesQueryResult(foundIncome);
        }
    }
}