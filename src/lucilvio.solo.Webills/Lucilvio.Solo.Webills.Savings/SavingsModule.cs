using System;
using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Savings.GetSavingsByFilter;
using Lucilvio.Solo.Webills.Savings.Infraestructure.DataAccess;
using Lucilvio.Solo.Webills.Savings.SaveMoney;

namespace Lucilvio.Solo.Webills.Savings
{
    public class SavingsModule
    {
        private readonly SavingsModuleConfiguration _configuration;

        public event Action<SavedMoney> MoneySaved;

        public SavingsModule(SavingsModuleConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public Task<GetSavingsByFilterOutput> GetSavingsByFilter(GetSavingsByFilterInput input)
        {
            if (input == null)
                throw new Error.InputNotInformed();

            return new GetSavingsByFilterComponent(new SavingsReadContext(this._configuration.ReadContextConnectionString))
                .Execute(input);
        }

        public async Task SaveMoney(SaveMoneyInput input)
        {
            if (input == null)
                throw new Error.InputNotInformed();

            var dataAccess = new SaveMoneyDataAccess(new SavingsContext(this._configuration.TransactionalContextConnectionString));
            var savedMoney = await new SaveMoneyComponent(dataAccess).Execute(input);

            this.MoneySaved?.Invoke(savedMoney);
        }

        class Error
        {
            public class InputNotInformed : Exception { }
        }
    }
}