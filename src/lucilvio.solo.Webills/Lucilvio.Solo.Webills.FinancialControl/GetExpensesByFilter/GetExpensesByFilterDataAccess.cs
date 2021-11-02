using System;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Lucilvio.Solo.Webills.FinancialControl.GetExpensesByFilter
{
    public class GetExpensesByFilterDataAccess
    {
        private readonly string _connectionString;

        public GetExpensesByFilterDataAccess(string connectionString)
        {
            this._connectionString = connectionString;
        }

        internal async Task<FoundExpenses> GetExpensesByFilter(Guid userId)
        {
            using (var connection = new SqlConnection(this._connectionString))
            {
                var sql = "select Id, Name, Category, Date, Value from Transactions.Expenses where UserId = @userId";

                var result = await connection.QueryAsync<FoundExpenses.Expense>(sql, new { userId });
                return new FoundExpenses(result);
            }
        }
    }
}