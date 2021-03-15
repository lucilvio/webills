using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.FinancialControl.Domain;

namespace Lucilvio.Solo.Webills.FinancialControl.EditIncome
{
    internal class EditIncomeComponent
    {
        private readonly IEditIncomeDataAccess _dataStorage;

        public EditIncomeComponent(IEditIncomeDataAccess dataStorage)
        {
            this._dataStorage = dataStorage ?? throw new ArgumentNullException(nameof(dataStorage));
        }

        public async Task Execute(EditIncomeInput input)
        {
            var foundIncome = await this._dataStorage.GetIncome(input.UserId);

            if (foundIncome == null)
                throw new Error.IncomeNotFound();

            foundIncome.Update(input.Name, input.Date, new TransactionValue(input.Value));

            await this._dataStorage.UpdateIncome(foundIncome);
        }

        internal class Error
        {
            internal class IncomeNotFound : Exception { }
        }
    }
}