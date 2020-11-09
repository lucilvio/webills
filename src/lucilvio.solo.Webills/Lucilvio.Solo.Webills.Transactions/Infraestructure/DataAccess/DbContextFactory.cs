using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Lucilvio.Solo.Webills.Transactions.Infraestructure.DataAccess
{
    internal class DbContextFactory : IDesignTimeDbContextFactory<TransactionsContext>
    {
        public TransactionsContext CreateDbContext(string[] args)
        {
            return new TransactionsContext("Server=.\\SQLEXPRESS;Database=lucilvio.solo.webills;Trusted_Connection=True;MultipleActiveResultSets=true;Connection Timeout=300;");
        }
    }
}
