using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Transactions.AddNewExpense;
using Lucilvio.Solo.Webills.Transactions.AddNewIncome;
using Lucilvio.Solo.Webills.Transactions.EditExpense;
using Lucilvio.Solo.Webills.Transactions.EditIncome;
using Lucilvio.Solo.Webills.Transactions.Infraestructure.DataAccess;
using Lucilvio.Solo.Webills.Transactions.RemoveExpense;
using Lucilvio.Solo.Webills.Transactions.RemoveIncome;

namespace Lucilvio.Solo.Webills.Transactions
{
    public class TransactionsModule : IAddNewExpenseUseCase, IAddNewIncomeUseCase, IEditExpenseUseCase, IEditIncomeUseCase, 
        IRemoveExpenseUseCase, IRemoveIncomeUseCase
    {
        private readonly string _connectionString;

        public TransactionsModule(string connectionString)
        {
            this._connectionString = connectionString;
        }

        internal TransactionsContext Context => new TransactionsContext();

        public async Task Execute(AddNewExpenseCommand command)
        {
            var dataAccess = new AddNewExpenseDataAccess(this.Context);
            await new AddNewExpenseUseCase(dataAccess).Execute(command);
        }

        public async Task Execute(AddNewIncomeCommand command)
        {
            var dataAccess = new AddNewIncomeDataAccess(this.Context);
            await new AddNewIncomeUseCase(dataAccess).Execute(command);
        }

        public async Task Execute(EditExpenseCommand command)
        {
            var dataAccess = new EditExpenseDataAccess(this.Context);
            await new EditExpenseUseCase(dataAccess).Execute(command);
        }

        public async Task Execute(EditIncomeCommand command)
        {
            var dataAccess = new EditIncomeDataAccess(this.Context);
            await new EditIncomeUseCase(dataAccess).Execute(command);
        }

        public async Task Execute(RemoveExpenseCommand command)
        {
            var dataAccess = new RemoveExpenseDataAccess(this.Context);
            await new RemoveExpenseUseCase(dataAccess).Execute(command);
        }

        public async Task Execute(RemoveIncomeCommand command)
        {
            var dataAccess = new RemoveIncomeDataAccess(this.Context);
            await new RemoveIncomeUseCase(dataAccess).Execute(command);
        }
    }
}
