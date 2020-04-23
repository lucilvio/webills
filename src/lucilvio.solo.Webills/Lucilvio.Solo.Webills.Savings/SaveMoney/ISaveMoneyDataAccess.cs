using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.Savings.Domain;

namespace Lucilvio.Solo.Webills.Savings.SaveMoney
{
    internal interface ISaveMoneyDataAccess
    {
        Task<User> GetUserById(Guid userId);
        Task Persist();
    }
}