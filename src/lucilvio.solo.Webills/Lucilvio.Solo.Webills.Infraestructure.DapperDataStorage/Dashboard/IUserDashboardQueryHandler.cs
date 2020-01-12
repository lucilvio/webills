using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.Infraestructure.DapperDataStorage
{
    public interface IUserDashboardQueryHandler
    {
        Task<UserDashboardQueryResult> Execute(UserDashboardQuery parameters);
    }
}