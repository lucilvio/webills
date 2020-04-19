using System;
using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Savings.GetSavingsByFilter;
using Lucilvio.Solo.Webills.Savings.SaveMoney;

namespace Lucilvio.Solo.Webills.Savings
{
    public class SavingsModule
    {
        public event Action<SavedMoney> MoneySaved;

        public async Task<GetSavingsByFilterOutput> GetSavingsByFilter(GetSavingsByFilterInput input)
        {
            if (input == null)
                throw new Error.InputNotInformed();

            return await new GetSavingsByFilterComponent().Execute(input);
        }

        public async Task SaveMoney(SaveMoneyInput input)
        {
            if (input == null)
                throw new Error.InputNotInformed();

            var savedMoney = await new SaveMoneyComponent().Execute(input);

            this.MoneySaved?.Invoke(savedMoney);
        }

        class Error
        {
            public class InputNotInformed : Exception { }
        }
    }
}
