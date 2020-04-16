using System.Data.Common;
using System.Data.SqlClient;

using Dapper;

namespace Lucilvio.Solo.Webills.Dashboard.Infraestructure.DataAccess
{
    internal class DashBoardContext
    {
        private readonly string _connectionString;

        public DashBoardContext()
        {
            this._connectionString = "Server=localhost;Database=lucilvio.solo.webills; Trusted_Connection=True; MultipleActiveResultSets=true; Connection Timeout=300;";

            this.CreateDatabaseIfNotExists();
        }

        private void CreateDatabaseIfNotExists()
        {
            using var connection = this.Connection;

            var sql = @$"
                if not exists ( select name from master.dbo.sysdatabases where name = N'lucilvio.solo.webills' )
	                create database [lucilvio.solo.webills]
                use [lucilvio.solo.webills]
                if not exists ( select name from sys.schemas where name = N'dashboard' )
                    exec sp_executesql N'create schema dashboard' 
                if not exists ( select * from [lucilvio.solo.webills].dbo.sysobjects where name = N'Transactions' and xType = 'U' )
	                create table dashboard.Transactions(UserId uniqueidentifier not null, Id uniqueidentifier not null, 
                    Name varchar(256) not null, Date datetime2 not null, Value decimal(18, 2) not null, Category int null, 
                    IsIncome bit not null, IsExpense bit not null)";

            connection.Execute(sql);
        }

        public DbConnection Connection => new SqlConnection(_connectionString);
    }
}