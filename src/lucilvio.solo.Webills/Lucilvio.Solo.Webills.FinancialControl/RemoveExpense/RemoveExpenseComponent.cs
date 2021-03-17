using System;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.FinancialControl.RemoveExpense
{
    internal class RemoveExpenseComponent
    {
        private readonly IRemoveExpenseDataAccess _dataAccess;

        public RemoveExpenseComponent(IRemoveExpenseDataAccess dataAccess)
        {
            this._dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
        }

        public async Task Execute(RemoveExpenseInput input)
        {
            var foundExpense = await this._dataAccess.GetExpense(input.UserId);

            if (foundExpense == null)
                throw new Error.ExpenseNotFound();

            await this._dataAccess.RemoveExpense(foundExpense);
        }

        internal class Error
        {
            internal class ExpenseNotFound : Exception { }
        }
    }
}