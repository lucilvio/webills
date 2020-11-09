using System.Threading.Tasks;
using Lucilvio.Solo.Webills.Transactions.AddNewExpense;
using Lucilvio.Solo.Webills.Transactions.AddNewIncome;
using Lucilvio.Solo.Webills.Transactions.CreateUser;
using Lucilvio.Solo.Webills.Transactions.GetUserDashboardInfo;
using Lucilvio.Solo.Webills.Transactions.GetUserTransactionsByFilter;

namespace Lucilvio.Solo.Webills.UserAccount
{
    internal interface IMessageDispatcher
    {
        Task<UserTransactions> DispatchGetUserTransactionsByFilterMessage(IGetUserTransactionsByFilterMessage message);
        Task DispatchAddNewIncomeMessage(IAddNewIncomeMessage message);
        Task DispatchCreateUserMessage(CreateUserMessage message);
        Task DispatchAddNewExpenseMessage(IAddNewExpenseMessage message);
        Task<UserFinancialInformation> DispatchGetUserFinancialInformation(IGetUserFinancialInformationMessage message);
    }
}