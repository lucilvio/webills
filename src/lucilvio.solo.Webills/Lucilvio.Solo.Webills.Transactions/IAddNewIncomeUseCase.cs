using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Transactions.AddNewIncome;

namespace Lucilvio.Solo.Webills.Transactions
{
    public interface IAddNewIncomeUseCase
    {
        Task Execute(AddNewIncomeCommand command);
    }
}