using System;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.Savings.SaveMoney
{
    internal class SaveMoneyComponent
    {
        private readonly ISaveMoneyDataAccess _dataAccess;

        public SaveMoneyComponent(ISaveMoneyDataAccess dataAccess)
        {
            this._dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
        }

        public async Task<SavedMoney> Execute(SaveMoneyInput input)
        {
            var foundUser = await this._dataAccess.GetUserById(input.UserId).ConfigureAwait(false);

            if (foundUser == null)
                throw new Error.UserNotFound();

            foundUser.SaveMoney(input.Value);

            await this._dataAccess.Persist();

            return new SavedMoney(input.UserId, input.Value);
        }

        class Error
        {
            internal class UserNotFound : Exception { }
        }
    }
}