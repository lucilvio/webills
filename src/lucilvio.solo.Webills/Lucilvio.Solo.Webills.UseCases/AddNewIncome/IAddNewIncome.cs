using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.UseCases.AddNewIncome
{
    public interface IAddNewIncome
    {
        Task Execute(AddNewIncomeCommand command);
    }
}