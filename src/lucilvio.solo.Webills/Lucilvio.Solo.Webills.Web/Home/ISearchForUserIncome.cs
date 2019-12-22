using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Lucilvio.Solo.Webills.Web.Home
{
    public interface IGetUserIncomesQueryHandler
    {
        Task<GetUserIncomeQueryResult> Execute(GetUserIncomesQueryByNumber query);
    }

    public class GetUserIncomeQueryHandler : IGetUserIncomesQueryHandler
    {
        private readonly WebillsContext _context;

        public GetUserIncomeQueryHandler(WebillsContext context)
        {
            this._context = context;
        }

        public async Task<GetUserIncomeQueryResult> Execute(GetUserIncomesQueryByNumber query)
        {
            var foundUser = await this._context.Users.AsNoTracking().Include(u => u.Incomes).FirstOrDefaultAsync();
            var foundIncome = foundUser.Incomes.FirstOrDefault(i => i.Number == query.Number);

            return new GetUserIncomeQueryResult(foundIncome);
        }
    }
}