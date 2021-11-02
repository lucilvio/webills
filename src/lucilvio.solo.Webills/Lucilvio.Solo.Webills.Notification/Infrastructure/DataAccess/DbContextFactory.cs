using Microsoft.EntityFrameworkCore.Design;

namespace Lucilvio.Solo.Webills.Notifications.Infrastructure.DataAccess
{
    internal class DbContextFactory : IDesignTimeDbContextFactory<NotificationDataContext>
    {
        public NotificationDataContext CreateDbContext(string[] args)
        {
            return new NotificationDataContext(args[0]);
        }
    }
}
