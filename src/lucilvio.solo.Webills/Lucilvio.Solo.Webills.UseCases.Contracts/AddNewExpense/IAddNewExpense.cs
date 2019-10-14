using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.UseCases.Contracts.AddNewExpense
{
    public interface IAddNewExpense
    {
        Task Execute(AddNewExpenseCommand command);
    }
}