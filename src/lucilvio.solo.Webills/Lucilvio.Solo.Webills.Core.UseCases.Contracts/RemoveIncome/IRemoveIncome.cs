using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.Core.UseCases.Contracts.RemoveIncome
{
    public interface IRemoveIncome
    {
        Task Execute(RemoveIncomeCommand command);
    }
}