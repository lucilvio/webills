using System;
using System.Data;
using System.Data.SqlClient;

namespace Lucilvio.Solo.Webills.Infraestructure.DapperDataStorage
{
    public class WebillsReadContext
    {
        private string _connectionString;
        
        public WebillsReadContext(string connectionString)
        {
            this._connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }


        internal IDbConnection GetConnection()
        {
            return new SqlConnection(this._connectionString);
        }
    }
}