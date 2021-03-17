using System;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.FinancialControl.RemoveIncome
{
    internal class RemoveIncomeComponent
    {
        private readonly IRemoveIncomeDataAccess _dataAccess;

        public RemoveIncomeComponent(IRemoveIncomeDataAccess dataAccess)
        {
            this._dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
        }

        public async Task Execute(RemoveIncomeInput input)
        {
            var foundIncome = await this._dataAccess.GetIncome(input.Id);

            if (foundIncome == null)
                throw new Error.IncomeNotFound();

            await this._dataAccess.RemoveIncome(foundIncome);
        }

        internal class Error
        {
            internal class IncomeNotFound : Exception { }
        }
    }
}