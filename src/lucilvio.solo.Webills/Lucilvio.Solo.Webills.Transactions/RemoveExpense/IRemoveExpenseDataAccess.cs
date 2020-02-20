using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.Transactions.Domain;

namespace Lucilvio.Solo.Webills.Transactions.RemoveExpense
{
    internal interface IRemoveExpenseDataAccess
    {
        Task<User> GetUserById(Guid id);
        Task Persist(Guid expenseId);
    }
}