using System.Threading.Tasks;
using Lucilvio.Solo.Webills.Transactions.EditIncome;

namespace Lucilvio.Solo.Webills.Transactions
{
    public interface IEditIncomeUseCase
    {
        Task Execute(EditIncomeCommand command);
    }
}