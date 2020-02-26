using System;
using System.Threading.Tasks;

using Dapper;

using Lucilvio.Solo.Webills.Transactions.Infraestructure.DataAccess;

namespace Lucilvio.Solo.Webills.Transactions.GetIncome
{
    internal class GetIncomeComponent : IComponent
    {
        private readonly TransactionsReadContext _context;

        public GetIncomeComponent(TransactionsReadContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<GetIncomeOutput> Execute(IGetIncomeInput input)
        {
            using (var connection = _context.Connection)
            {
                var sql = "select Id, Name, Category, Date, Value from Transactions.Expenses where Id = @id";

                return await connection.QueryFirstOrDefaultAsync<GetIncomeOutput>(sql, new { id = input.Id });
            }
        }
    }
}
