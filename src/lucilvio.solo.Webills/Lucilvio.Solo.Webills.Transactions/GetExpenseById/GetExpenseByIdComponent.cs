using System;
using System.Threading.Tasks;

using Dapper;

using Lucilvio.Solo.Webills.Transactions.Infraestructure.DataAccess;

namespace Lucilvio.Solo.Webills.Transactions.GetExpense
{
    internal class GetExpenseByIdComponent
    {
        private readonly TransactionsReadContext _context;

        public GetExpenseByIdComponent(TransactionsReadContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<GetExpenseByIdOutput> Execute(GetExpenseByIdInput input)
        {
            using (var connection = this._context.Connection)
            {
                var sql = "select Id, Name, Category, Date, Value from Transactions.Expenses where Id = @id";

                return await connection.QueryFirstOrDefaultAsync<GetExpenseByIdOutput>(sql, new { id = input.Id });
            }
        }
    }
}