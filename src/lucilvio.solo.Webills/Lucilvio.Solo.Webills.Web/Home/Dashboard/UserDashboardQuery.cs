using System;

namespace Lucilvio.Solo.Webills.Web.Home
{
    public class UserDashboardQuery
    {
        public UserDashboardQuery(Guid login)
        {
            this.UserId = login;
        }

        public Guid UserId { get; }
    }
}