using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.Clients.Web.Incomes
{
    public interface IGetUserIncomesByFilterQueryHandler
    {
        Task<GetUserIncomesByFilterQueryResult> Execute(GetUserIncomesByFilterQuery getUserIncomesByFilterQuery);
    }
}