using Lucilvio.Solo.Webills.Core.Domain.User;
using System;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.Core.UseCases.EditExpense
{
    public interface IEditExpenseDataStorage
    {
        Task<User> GetUser();
        Task Persist(Guid incomeNumber, User foundUser);
    }
}