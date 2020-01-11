using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.Domain.User;

namespace Lucilvio.Solo.Webills.UseCases.AddNewExpense
{
    public interface IAddNewExpenseDataStorage
    {
        Task<User> GetUserById(Guid id);
        Task Persist(User user);
    }
}