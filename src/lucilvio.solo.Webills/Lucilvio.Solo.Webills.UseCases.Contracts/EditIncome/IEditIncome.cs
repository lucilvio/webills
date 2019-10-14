using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.UseCases.Contracts.EditIncome
{
    public interface IEditIncome
    {
        Task Execute(EditIncomeCommand editIncomeCommand);
    }
}