namespace Lucilvio.Solo.Webills.Web.Home
{
    public class UserDashboardQuery
    {
        public UserDashboardQuery(int userId)
        {
            this.UserId = userId;
        }

        public int UserId { get; }
    }
}