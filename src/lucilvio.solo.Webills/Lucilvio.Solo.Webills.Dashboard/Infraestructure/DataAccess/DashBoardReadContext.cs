using System.Data.Common;
using System.Data.SqlClient;

using Dapper;

namespace Lucilvio.Solo.Webills.Dashboard.Infraestructure.DataAccess
{
    internal class DashBoardReadContext
    {
        private readonly string _connectionString;

        public DashBoardReadContext()
        {
            this._connectionString = "Server=localhost;Database=lucilvio.solo.webills; Trusted_Connection=True; MultipleActiveResultSets=true; Connection Timeout=300;";

            this.CreateDatabaseIfNotExists();
        }

        private void CreateDatabaseIfNotExists()
        {
            using (var connection = this.Connection)
            {
                var sql =
                    @$"if not exists ( select name from master.dbo.sysdatabases where name = N'lucilvio.solo.webills' )
                    create database [lucilvio.solo.webills] go
                    if not exists ( select * from master.dbo.sysobjects where name = N'Transactions' and xType = 'U' )
                    create table Expenses(UserId uniqueidentifier not null, Name varchar(256) not null, 
                    Date datetime2 not null, Value decimal(18, 2) not null, Category int not null, Income bit not null,
                    Expense bit not null) go";

                connection.Execute(sql);
            }
        }

        public DbConnection Connection => new SqlConnection(_connectionString);
    }
}