using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.Transactions.Domain;

namespace Lucilvio.Solo.Webills.Transactions.AddNewExpense
{
    interface IAddNewExpenseDataAccess
    {
        Task<User> GetUserById(Guid userId);
        Task Persist();
    }
}