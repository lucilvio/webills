using System.Data.Common;

using Microsoft.Data.SqlClient;

namespace Lucilvio.Solo.Webills.Savings.Infraestructure.DataAccess
{
    public class SavingsReadContext
    {
        private readonly string _connectionString;

        public SavingsReadContext(string connectionString)
        {
            this._connectionString = connectionString;
            //this._connectionString = "Server=localhost;Database=lucilvio.solo.webills; Trusted_Connection=True; MultipleActiveResultSets=true; Connection Timeout=300;";
        }

        public DbConnection Connection => new SqlConnection(this._connectionString);
    }
}
