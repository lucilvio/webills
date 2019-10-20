using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.UseCases.Contracts.RemoveIncome
{
    public interface IRemoveIncome
    {
        Task Execute(RemoveIncomeCommand command);
    }
}