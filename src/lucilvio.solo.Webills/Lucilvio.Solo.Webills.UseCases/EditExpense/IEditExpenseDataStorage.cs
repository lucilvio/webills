using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.Domain.User;

namespace Lucilvio.Solo.Webills.UseCases.EditExpense
{
    public interface IEditExpenseDataStorage
    {
        Task<User> GetUser();
        Task Persist(Guid incomeNumber, User foundUser);
    }
}