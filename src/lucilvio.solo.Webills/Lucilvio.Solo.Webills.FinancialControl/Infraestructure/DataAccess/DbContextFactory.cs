using Microsoft.EntityFrameworkCore.Design;

namespace Lucilvio.Solo.Webills.FinancialControl.Infraestructure.DataAccess
{
    internal class DbContextFactory : IDesignTimeDbContextFactory<FinancialControlDataContext>
    {
        public FinancialControlDataContext CreateDbContext(string[] args)
        {
            return new FinancialControlDataContext("Server=localhost;Database=lucilvio.solo.webills;Trusted_Connection=True;MultipleActiveResultSets=true;Connection Timeout=300;");
        }
    }
}