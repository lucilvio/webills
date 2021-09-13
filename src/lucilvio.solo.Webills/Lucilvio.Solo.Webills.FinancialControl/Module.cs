using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.FinancialControl.AddNewExpense;
using Lucilvio.Solo.Webills.FinancialControl.AddNewIncome;
using Lucilvio.Solo.Webills.FinancialControl.AddNewRecurrentExpense;
using Lucilvio.Solo.Webills.FinancialControl.AddNewRecurrentIncome;
using Lucilvio.Solo.Webills.FinancialControl.DeleteIncome;
using Lucilvio.Solo.Webills.FinancialControl.GetExpenseCategories;
using Lucilvio.Solo.Webills.FinancialControl.GetIncomeCategories;
using Lucilvio.Solo.Webills.FinancialControl.GetUserDashboardInfo;
using Lucilvio.Solo.Webills.FinancialControl.GetUserFinancialInformation;
using Lucilvio.Solo.Webills.FinancialControl.GetUserTransactionsByFilter;

namespace Lucilvio.Solo.Webills.FinancialControl
{
    public class Module
    {
        private readonly Configurations _configurations;

        public Module(Configurations configurations)
        {
            this._configurations = configurations ?? throw new System.ArgumentNullException(nameof(configurations));
        }

        public async Task<UserFinancialInformation> GetUserFinancialInfo(GetUserFinancialInformationMessage message)
        {
            return await new GetUserFinancialInformationMessageDispatcher().Dispatch(message, this._configurations);
        }

        public async Task AddNewExpense(AddNewExpenseMessage message)
        {
            await new AddNewExpenseMessageDispatcher().Dispatch(message, this._configurations);
        }

        public async Task AddNewRecurrentExpense(AddNewRecurrentExpenseMessage message)
        {
            await new AddNewRecurrentExpenseMessageDispatcher().Dispatch(message, this._configurations);
        }

        public async Task AddNewRecurrentIncome(AddNewRecurrentIncomeMessage message)
        {
            await new AddNewRecurrentIncomeMessageDispatcher().Dispatch(message, this._configurations);
        }

        public async Task AddNewIncome(AddNewIncomeMessage message)
        {
            await new AddNewIncomeMessageDispatcher().Dispatch(message, this._configurations);
        }

        public async Task<UserTransactions> GetUserTransactionsByFilter(GetUserTransactionsByFilterMessage message)
        {
            return await new GetUserTransactionsByFilterMessageDispatcher().Dispatch(message, this._configurations);
        }


        public async Task<ExpenseCategories> GetExpenseCategories(GetExpenseCategoriesMessage message)
        {
            return await new GetExpenseCategoriesMessageDispatcher().Dispatch(message);
        }

        public async Task<IncomeCategories> GetIncomeCategories(GetIncomeCategoriesMessage message)
        {
            return await new GetIncomeCategoriesMessageDispatcher().Dispatch(message);
        }

        public async Task DeleteIncome(DeleteIncomeMessage message)
        {
            await new DeleteIncomeMessageDispatcher().Dispatch(message, this._configurations);
        }
    }
}