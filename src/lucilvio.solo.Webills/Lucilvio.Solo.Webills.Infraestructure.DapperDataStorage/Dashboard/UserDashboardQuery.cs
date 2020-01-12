using System;

namespace Lucilvio.Solo.Webills.Infraestructure.DapperDataStorage
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