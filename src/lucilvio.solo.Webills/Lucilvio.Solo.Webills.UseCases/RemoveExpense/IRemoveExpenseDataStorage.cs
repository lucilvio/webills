using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.Domain.User;

namespace Lucilvio.Solo.Webills.UseCases.RemoveExpense
{
    public interface IRemoveExpenseDataStorage
    {
        Task<User> GetUserById(Guid id);
        Task Persist(Guid expenseId);
    }
}