using Microsoft.EntityFrameworkCore.Design;

namespace Lucilvio.Solo.Webills.FinancialControl.Infraestructure.DataAccess
{
    internal class DbContextFactory : IDesignTimeDbContextFactory<FinancialControlDataContext>
    {
        public FinancialControlDataContext CreateDbContext(string[] args)
        {
            return new FinancialControlDataContext(new Configurations
            {
                DataConnectionString = args[0]
            });
        }
    }
}