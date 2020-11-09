using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.EventBus;
using Lucilvio.Solo.Webills.Transactions.AddNewExpense;
using Lucilvio.Solo.Webills.Transactions.AddNewIncome;
using Lucilvio.Solo.Webills.Transactions.GetUserDashboardInfo;
using Lucilvio.Solo.Webills.Transactions.GetUserTransactionsByFilter;
using Lucilvio.Solo.Webills.UserAccount;
using Lucilvio.Solo.Webills.UserAccount.Infraestructure;

namespace Lucilvio.Solo.Webills.Transactions
{
    public class Module
    {
        private readonly IEventBus _eventBus;
        private readonly IMessageDispatcher _messageDispatcher;

        public Module(IEventBus eventBus, Configurations configurations)
        {
            this._eventBus = eventBus;
            this._messageDispatcher = new DefaultMessageDispatcher(configurations, eventBus);

            this._eventBus.Subscibe("UserAccountCreated", async userAccount =>
            {
                await this._messageDispatcher.DispatchCreateUserMessage(new CreateUser.CreateUserMessage(userAccount.UserId));
            });
        }

        public async Task<UserFinancialInformation> GetUserFinancialInfo(IGetUserFinancialInformationMessage message)
        {
            return await this._messageDispatcher.DispatchGetUserFinancialInformation(message);
        }

        public async Task AddNewExpense(IAddNewExpenseMessage message)
        {
            await this._messageDispatcher.DispatchAddNewExpenseMessage(message);
        }

        public async Task AddNewIncome(IAddNewIncomeMessage message)
        {
            await this._messageDispatcher.DispatchAddNewIncomeMessage(message);
        }

        public async Task<UserTransactions> GetUserTransactionsByFilter(IGetUserTransactionsByFilterMessage message)
        {
            return await this._messageDispatcher.DispatchGetUserTransactionsByFilterMessage(message);
        }
    }
}