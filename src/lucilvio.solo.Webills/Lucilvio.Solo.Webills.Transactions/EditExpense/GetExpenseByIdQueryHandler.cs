using System;
using System.Threading.Tasks;

using Dapper;

using Lucilvio.Solo.Webills.Transactions.Infraestructure.DataAccess;

namespace Lucilvio.Solo.Webills.Transactions.EditExpense
{
    internal class GetExpenseByIdQueryHandler : IQueryHandler<GetExpenseByIdQuery, GetExpenseByIdQueryResult>
    {
        private readonly TransactionsReadContext _context;

        public GetExpenseByIdQueryHandler(TransactionsReadContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<GetExpenseByIdQueryResult> Handle(GetExpenseByIdQuery query)
        {
            using (var connection = this._context.Connection)
            {
                var sql = "select Id, Name, Category, Date, Value from Transactions.Expenses where Id = @id";

                return await connection.QueryFirstOrDefaultAsync<GetExpenseByIdQueryResult>(sql, new { id = query.Id });
            }
        }
    }
}
