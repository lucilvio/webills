using System.Threading.Tasks;
using Lucilvio.Solo.Webills.EventBus;
using Lucilvio.Solo.Webills.Transactions;
using Lucilvio.Solo.Webills.Transactions.AddNewExpense;
using Lucilvio.Solo.Webills.Transactions.AddNewIncome;
using Lucilvio.Solo.Webills.Transactions.CreateUser;
using Lucilvio.Solo.Webills.Transactions.GetUserDashboardInfo;
using Lucilvio.Solo.Webills.Transactions.GetUserFinancialInformation;
using Lucilvio.Solo.Webills.Transactions.GetUserTransactionsByFilter;
using Lucilvio.Solo.Webills.Transactions.Infraestructure.DataAccess;
using Microsoft.Data.SqlClient;

namespace Lucilvio.Solo.Webills.UserAccount.Infraestructure
{
    internal class DefaultMessageDispatcher : IMessageDispatcher
    {
        private readonly IEventBus _eventBus;
        private readonly Configurations _configuration;

        public DefaultMessageDispatcher(Configurations configuration, IEventBus eventBus)
        {
            this._configuration = configuration;
            this._eventBus = eventBus;
        }

        public async Task DispatchAddNewExpenseMessage(IAddNewExpenseMessage message)
        {
            using var context = new TransactionsContext(this._configuration.DataConnection);

            var dataAccess = new AddNewExpenseDataAccess(context);
            var handler = new AddNewExpenseMessageHandler(dataAccess, this._eventBus);

            await handler.Execute(message);
        }

        public async Task DispatchAddNewIncomeMessage(IAddNewIncomeMessage message)
        {
            using var context = new TransactionsContext(this._configuration.DataConnection);

            var dataAccess = new AddNewIncomeDataAccess(context);
            var handler = new AddNewIncomeMessageHandler(dataAccess, this._eventBus);

            await handler.Execute(message);
        }

        public async Task DispatchCreateUserMessage(CreateUserMessage message)
        {
            using var context = new TransactionsContext(this._configuration.DataConnection);

            var dataAccess = new CreateUserDataAccess(context);
            var handler = new CreateUserMessageHandler(dataAccess);

            await handler.Execute(message);
        }

        public async Task<UserFinancialInformation> DispatchGetUserFinancialInformation(IGetUserFinancialInformationMessage message)
        {
            using var dbConnection = new SqlConnection(this._configuration.DataConnection);

            var handler = new GetUserFinancialInformationMessageHandler(dbConnection);
            return await handler.Execute(message);
        }

        public async Task<UserTransactions> DispatchGetUserTransactionsByFilterMessage(IGetUserTransactionsByFilterMessage message)
        {
            using var dbConnection = new SqlConnection(this._configuration.DataConnection);

            var handler = new GetUserTransactionsByFilterMessageHandler(dbConnection);
            return await handler.Execute(message);
        }
    }
}
