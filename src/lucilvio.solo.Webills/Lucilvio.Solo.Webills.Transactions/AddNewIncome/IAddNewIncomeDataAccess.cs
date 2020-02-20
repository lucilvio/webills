using System;
using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Transactions.Domain;

namespace Lucilvio.Solo.Webills.Transactions.AddNewIncome
{
    internal interface IAddNewIncomeDataAccess
    {
        Task<User> GetUserById(Guid id);
        Task Persist();
    }
}