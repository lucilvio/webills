using System.Linq;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.Infraestructure.DapperDataStorage
{
    public interface IGetUserIncomesByFilterQueryHandler
    {
        Task<GetUserIncomesByFilterQueryResult> Execute(GetUserIncomesByFilterQuery getIncomesByFilterQuery);
    }
}