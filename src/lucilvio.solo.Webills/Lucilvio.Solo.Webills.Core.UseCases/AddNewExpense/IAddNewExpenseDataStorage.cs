using System;
using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Core.Domain.User;

namespace Lucilvio.Solo.Webills.Core.UseCases.AddNewExpense
{
    public interface IAddNewExpenseDataStorage
    {
        Task<User> GetUserById(Guid id);
        Task Persist(User user);
    }
}