using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.Web.Home.Sample
{
    public interface IUserDashboardQueryHandler
    {
        Task<UserDashboardQueryResult> Execute(UserDashboardQuery parameters);
    }
}