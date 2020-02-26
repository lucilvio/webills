using System;
using System.Threading.Tasks;

using Dapper;

using Lucilvio.Solo.Webills.Transactions.Infraestructure.DataAccess;

namespace Lucilvio.Solo.Webills.Transactions.GetExpense
{
    internal class GetExpenseComponent : IComponent
    {
        private readonly TransactionsReadContext _context;

        public GetExpenseComponent(TransactionsReadContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<GetExpenseOutput> Execute(IGetExpenseInput input)
        {
            using (var connection = _context.Connection)
            {
                var sql = "select Id, Name, Category, Date, Value from Transactions.Expenses where Id = @id";

                return await connection.QueryFirstOrDefaultAsync<GetExpenseOutput>(sql, new { id = input.Id });
            }
        }
    }
}
