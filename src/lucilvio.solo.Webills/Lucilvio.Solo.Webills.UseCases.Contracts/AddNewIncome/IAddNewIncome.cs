using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.UseCases.Contracts.AddNewIncome
{
    public interface IAddNewIncome
    {
        Task Execute(AddNewIncomeCommand command);
    }
}