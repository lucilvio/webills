using System;
using System.Threading.Tasks;

using Dapper;
using Lucilvio.Solo.Webills.Dashboard.Infraestructure.DataAccess;

namespace Lucilvio.Solo.Webills.Dashboard.MainDashboard
{
    internal class GetUserDashboardQueryHandler : IQueryHandler<GetUserDashboardQuery, GetUserDashboardQueryResult>
    {
        private readonly DashBoardReadContext _context;

        public GetUserDashboardQueryHandler(DashBoardReadContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<GetUserDashboardQueryResult> Handle(GetUserDashboardQuery query)
        {
            using (var connection = this._context.Connection)
            {
                var sql = "";

                return await connection.QueryFirstOrDefaultAsync<GetUserDashboardQueryResult>(sql, new { userId = query.UserId });
            }
        }
    }
}
