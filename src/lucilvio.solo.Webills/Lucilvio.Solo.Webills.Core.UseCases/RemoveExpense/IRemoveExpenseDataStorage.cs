using System;
using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Core.Domain.User;

namespace Lucilvio.Solo.Webills.Core.UseCases.RemoveExpense
{
    public interface IRemoveExpenseDataStorage
    {
        Task<User> GetUserById(Guid id);
        Task Persist(Guid expenseId);
    }
}