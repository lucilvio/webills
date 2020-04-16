using System.Threading.Tasks;

using Dapper;

using Lucilvio.Solo.Webills.Dashboard.Infraestructure.DataAccess;

namespace Lucilvio.Solo.Webills.Dashboard.EditTransaction
{
    internal class EditTransactionComponent
    {
        private readonly DashBoardContext _context;

        public EditTransactionComponent(DashBoardContext context)
        {
            this._context = context;
        }

        public async Task Execute(EditTransactionInput input)
        {
            using (var connection = this._context.Connection)
            {
                var sql = @"update dashboard.Transactions set name = @name, date = @date, value = @value, 
                    category = @category where id = @id and userId = @userId";

                await connection.ExecuteAsync(sql, new
                {
                    id = input.Id,
                    date = input.Date,
                    name = input.Name,
                    value = input.Value,
                    userId = input.UserId,
                    category = input.Category
                });
            }
        }
    }
}