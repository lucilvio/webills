using System;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.Savings.SaveMoney
{
    internal class SaveMoneyComponent
    {
        public async Task<SavedMoney> Execute(SaveMoneyInput input, Func<SavedMoney, Task> onSaveMoney = null)
        {
            return new SavedMoney(input.UserId, input.Value);
        }
    }
}
