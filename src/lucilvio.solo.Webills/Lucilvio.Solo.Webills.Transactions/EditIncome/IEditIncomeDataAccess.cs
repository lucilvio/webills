using System;
using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Transactions.Domain;

namespace Lucilvio.Solo.Webills.Transactions.EditIncome
{
    internal interface IEditIncomeDataAccess
    {
        Task<User> GetUserById(Guid id);
        Task Persist();
    }
}