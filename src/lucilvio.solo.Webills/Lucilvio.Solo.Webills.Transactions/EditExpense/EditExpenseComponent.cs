using System;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.FinancialControl.EditExpense
{
    internal class EditExpenseComponent
    {
        private readonly IEditExpenseDataAccess _dataStorage;

        public EditExpenseComponent(IEditExpenseDataAccess dataStorage)
        {
            this._dataStorage = dataStorage ?? throw new ArgumentNullException(nameof(dataStorage));
        }

        public async Task Execute(EditExpenseInput input)
        {
            var foundExpense = await this._dataStorage.GetExpense(input.UserId);

            if (foundExpense == null)
                throw new Error.ExpenseNotFound();

            foundExpense.Update(input.UserId, input.Name, input.Category, input.Date, input.Value);                

            await this._dataStorage.UpdateExpense(foundExpense);
        }

        internal class Error
        {
            internal class ExpenseNotFound : Exception { }
        }
    }
}