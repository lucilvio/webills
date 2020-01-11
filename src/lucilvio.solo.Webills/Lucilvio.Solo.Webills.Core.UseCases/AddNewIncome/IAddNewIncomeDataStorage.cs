using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Core.Domain.User;

namespace Lucilvio.Solo.Webills.Core.UseCases.AddNewIncome
{
    public interface IAddNewIncomeDataStorage
    {
        Task<User> GetUser();
        Task Persist(User user);
    }
}