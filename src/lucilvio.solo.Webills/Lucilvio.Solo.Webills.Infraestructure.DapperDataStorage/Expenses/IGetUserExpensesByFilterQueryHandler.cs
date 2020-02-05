using System.Linq;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.Infraestructure.DapperDataStorage
{
    public interface IGetUserExpensesByFilterQueryHandler
    {
        Task<GetUserExpensesByFilterQueryResult> Execute(GetUserExpensesByFilterQuery getExpensesByFilterQuery);
    }
}