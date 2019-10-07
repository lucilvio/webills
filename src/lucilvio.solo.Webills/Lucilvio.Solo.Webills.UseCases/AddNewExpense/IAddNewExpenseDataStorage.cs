using System.Threading.Tasks;
using Lucilvio.Solo.Webills.Domain.User;

namespace Lucilvio.Solo.Webills.UseCases.AddNewExpense
{
    public interface IAddNewExpenseDataStorage
    {
        User GetUser();
        Task Persist(User user);
    }
}