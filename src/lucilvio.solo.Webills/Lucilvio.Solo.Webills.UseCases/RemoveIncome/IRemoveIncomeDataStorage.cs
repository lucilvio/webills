using System.Threading.Tasks;
using Lucilvio.Solo.Webills.Domain.User;

namespace Lucilvio.Solo.Webills.UseCases.RemoveIncome
{
    public interface IRemoveIncomeDataStorage
    {
        Task<User> GetUser();
        Task Persist();
    }
}