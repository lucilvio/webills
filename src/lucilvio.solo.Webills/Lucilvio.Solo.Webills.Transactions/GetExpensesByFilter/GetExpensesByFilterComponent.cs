using System;
using System.Threading.Tasks;

using Dapper;

using Lucilvio.Solo.Webills.FinancialControl.Infraestructure.DataAccess;

namespace Lucilvio.Solo.Webills.FinancialControl.GetExpensesByFilter
{
    internal class GetExpensesByFilterComponent
    {
        private readonly TransactionsReadContext _context;

        public GetExpensesByFilterComponent(TransactionsReadContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<GetExpensesByFilterOutput> Execute(GetExpensesByFilterInput input)
        {
            using (var connection = this._context.Connection)
            {
                var sql = "select Id, Name, Category, Date, Value from Transactions.Expenses where UserId = @userId";

                var result = await connection.QueryAsync<GetExpensesByFilterOutput.Expense>(sql, new { input.UserId });
                return new GetExpensesByFilterOutput(result);
            }
        }
    }
}