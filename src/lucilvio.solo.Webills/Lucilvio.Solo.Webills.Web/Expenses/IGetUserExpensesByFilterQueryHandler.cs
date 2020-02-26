using System.Threading.Tasks;
using Lucilvio.Solo.Webills.Clients.Web.Expenses.Index;

namespace Lucilvio.Solo.Webills.Clients.Web.Expenses
{
    public interface IGetUserExpensesByFilterQueryHandler
    {
        Task<GetUserExpensesByFilterQueryResult> Execute(GetUserExpensesByFilterQuery getUserExpensesByFilterQuery);
    }
}