using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace Lucilvio.Solo.Webills.FinancialControl.GetUserTransactionsByFilter
{
    public record GetUserTransactionsByFilterMessage(Guid UserId);

    internal class GetUserTransactionsByFilterMessageHandler
    {
        private readonly IDbConnection _dbConnection;

        public GetUserTransactionsByFilterMessageHandler(IDbConnection dbConnection)
        {
            this._dbConnection = dbConnection;
        }

        public async Task<dynamic> Execute(GetUserTransactionsByFilterMessage message)
        {
            var sql = @"select Id, RecurrentExpenseId as RecurrencyId, Name, Date, UserId, Value, 'Expense' Type from financialControl.Expenses
                where userId = @userId
                UNION select Id, RecurrentIncomeId as RecurrencyId, Name, Date, UserId, Value, 'Income' Type from financialControl.Incomes
                where userId = @userId
                order by Date asc";

            var transactions = await this._dbConnection.QueryAsync<UserTransactions.Transaction>(sql, new { message.UserId });
            this._dbConnection.Dispose();

            return new UserTransactions(transactions);
        }
    }
}