using System.Data.Common;

using Microsoft.Data.SqlClient;

namespace Lucilvio.Solo.Webills.Transactions.Infraestructure.DataAccess
{
    internal class TransactionsReadContext
    {
        private readonly string _connectionString;

        public TransactionsReadContext()
        {
            this._connectionString = @"Server=.\SQLEXPRESS;Database=lucilvio.solo.webills; Trusted_Connection=True; MultipleActiveResultSets=true; Connection Timeout=300;";
        }

        public DbConnection Connection => new SqlConnection(this._connectionString);
    }
}