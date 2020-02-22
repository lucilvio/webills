using System.Threading.Tasks;

using Dapper;

using Lucilvio.Solo.Webills.Transactions.Infraestructure.DataAccess;

namespace Lucilvio.Solo.Webills.Transactions.GetIncomeById
{
    internal class GetIncomeByIdQueryHandler : IQueryHandler<GetIncomeByIdQuery, GetIncomeByIdQueryResult>
    {
        private readonly TransactionsReadContext _context;

        public GetIncomeByIdQueryHandler(TransactionsReadContext context)
        {
            this._context = context;
        }

        public async Task<GetIncomeByIdQueryResult> Handle(GetIncomeByIdQuery query)
        {
            using (var connection = this._context.Connection)
            {
                var sql = "select Id, Name, Date, Value from Transactions.Incomes where Id = @id";

                return await connection.QueryFirstOrDefaultAsync<GetIncomeByIdQueryResult>(sql, new { id = query.Id });
            }
        }
    }
}
