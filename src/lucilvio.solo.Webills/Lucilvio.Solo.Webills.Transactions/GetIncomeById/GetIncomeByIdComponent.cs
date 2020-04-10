using System;
using System.Threading.Tasks;

using Dapper;

using Lucilvio.Solo.Webills.Transactions.Infraestructure.DataAccess;

namespace Lucilvio.Solo.Webills.Transactions.GetIncomeById
{
    internal class GetIncomeByIdComponent
    {
        private readonly TransactionsReadContext _context;

        public GetIncomeByIdComponent(TransactionsReadContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<GetIncomeByIdOutput> Execute(GetIncomeByIdInput input)
        {
            using (var connection = this._context.Connection)
            {
                var sql = "select Id, Name, Date, Value from Transactions.Incomes where Id = @id";

                return await connection.QueryFirstOrDefaultAsync<GetIncomeByIdOutput>(sql, new { id = input.Id });
            }
        }
    }
}
