using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Core.Domain.User;

namespace Lucilvio.Solo.Webills.Core.UseCases.RemoveIncome
{
    public interface IRemoveIncomeDataStorage
    {
        Task<User> GetUser();
        Task Persist();
    }
}