using System.Threading.Tasks;

using Dapper;

using Lucilvio.Solo.Webills.Dashboard.Infraestructure.DataAccess;

namespace Lucilvio.Solo.Webills.Dashboard.RemoveTransaction
{
    internal class RemoveTransactionComponent
    {
        private readonly DashBoardContext _context;

        public RemoveTransactionComponent(DashBoardContext context)
        {
            this._context = context;
        }

        public async Task Execute(RemoveTransactionInput input)
        {
            using (var connection = this._context.Connection)
            {
                var sql = @"delete from dashboard.Transactions where id = @id and userId = @userId";

                await connection.ExecuteAsync(sql, new
                {
                    id = input.Id,
                    userId = input.UserId,
                });
            }
        }
    }
}