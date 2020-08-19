using System;

namespace Lucilvio.Solo.Webills.Transactions.GetUserDashboardInfo
{
    public class GetDashboardInfoByFilterInput
    {
        public GetDashboardInfoByFilterInput(Guid userId)
        {
            this.UserId = userId;
        }

        internal Guid UserId { get; }
    }
}