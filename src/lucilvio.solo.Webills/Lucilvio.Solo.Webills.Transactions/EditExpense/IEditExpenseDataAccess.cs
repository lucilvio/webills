using System;
using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Transactions.Domain;

namespace Lucilvio.Solo.Webills.Transactions.EditExpense
{
    internal interface IEditExpenseDataAccess
    {
        Task<User> GetUserById(Guid id);
        Task Persist();
    }
}