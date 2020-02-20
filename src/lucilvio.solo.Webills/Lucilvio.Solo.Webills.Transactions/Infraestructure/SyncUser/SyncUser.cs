using System;
using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Transactions.Domain;

namespace Lucilvio.Solo.Webills.Transactions.Infraestructure.SyncUser
{
    internal class SyncUser : ISyncUser
    {
        private readonly ISyncUserDataAccess _dataAccess;

        public SyncUser(ISyncUserDataAccess dataAccess)
        {
            this._dataAccess = dataAccess;
        }

        public async Task Execute(Guid id)
        {
            var foundUser = await this._dataAccess.GetUserById(id);

            if (foundUser == null)
                await this._dataAccess.InsertUser(new User(id));
        }
    }
}
