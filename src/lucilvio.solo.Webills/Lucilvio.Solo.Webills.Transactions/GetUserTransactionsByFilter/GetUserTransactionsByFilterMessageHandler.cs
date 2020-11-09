using Dapper;
using System.Data;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.Transactions.GetUserTransactionsByFilter
{
    internal class GetUserTransactionsByFilterMessageHandler
    {
        private readonly IDbConnection _dbConnection;

        public GetUserTransactionsByFilterMessageHandler(IDbConnection dbConnection)
        {
            this._dbConnection = dbConnection;
        }

        public async Task<dynamic> Execute(IGetUserTransactionsByFilterMessage message)
        {
            var sql = @"select Id, Name, Date, UserId, Value, 'Expense' Type from transactions.Expenses
                UNION select Id, Name, Date, UserId, Value, 'Income' Type from transactions.Incomes
                where userId = @userId";

            var transactions = await this._dbConnection.QueryAsync<UserTransactions.Transaction>(sql, new { message.UserId });

            return new UserTransactions(transactions);
        }
    }
}
