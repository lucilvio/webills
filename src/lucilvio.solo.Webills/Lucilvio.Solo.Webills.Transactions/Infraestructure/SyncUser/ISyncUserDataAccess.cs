using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.Transactions.Domain;

namespace Lucilvio.Solo.Webills.Transactions.Infraestructure.SyncUser
{
    internal interface ISyncUserDataAccess
    {
        Task<User> GetUserById(Guid id);
        Task InsertUser(User user);
    }
}