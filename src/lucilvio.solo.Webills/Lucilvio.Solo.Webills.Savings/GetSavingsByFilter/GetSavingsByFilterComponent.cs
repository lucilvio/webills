using Dapper;

using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Savings.Infraestructure.DataAccess;

namespace Lucilvio.Solo.Webills.Savings.GetSavingsByFilter
{
    internal class GetSavingsByFilterComponent
    {
        private readonly SavingsReadContext _readContext;

        public GetSavingsByFilterComponent(SavingsReadContext readContext)
        {
            this._readContext = readContext ?? throw new System.ArgumentNullException(nameof(readContext));
        }

        internal async Task<GetSavingsByFilterOutput> Execute(GetSavingsByFilterInput input)
        {
            using (var con = this._readContext.Connection)
            {
                var total = await this._readContext.Connection.ExecuteScalarAsync<decimal>(
                    @"select sum(value) from savings.Transactions t inner join 
                    savings.SavingsAccounts sa on t.SavingsAccountId = sa.Id where sa.Userid = @userId 
                    group by t.SavingsAccountId", new { userId = input.UserId }).ConfigureAwait(false);

                return new GetSavingsByFilterOutput(total);
            }
        }
    }
}