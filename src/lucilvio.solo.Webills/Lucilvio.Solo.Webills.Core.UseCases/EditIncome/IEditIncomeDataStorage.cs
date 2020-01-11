using System;
using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Core.Domain.User;

namespace Lucilvio.Solo.Webills.Core.UseCases.EditIncome
{
    public interface IEditIncomeDataStorage
    {
        Task<User> GetUser();
        Task Persist(Guid incomeNumber, User foundUser);
    }
}