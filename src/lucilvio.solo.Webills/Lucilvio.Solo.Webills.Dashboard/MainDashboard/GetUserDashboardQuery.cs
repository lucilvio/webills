using System;

namespace Lucilvio.Solo.Webills.Dashboard.MainDashboard
{
    public class GetUserDashboardQuery : IQuery
    {
        public GetUserDashboardQuery(Guid userId)
        {
            this.UserId = userId;
        }

        public Guid UserId { get; }
    }
}