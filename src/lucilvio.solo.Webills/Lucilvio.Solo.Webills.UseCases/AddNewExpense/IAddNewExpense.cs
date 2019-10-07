using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.UseCases.AddNewExpense
{
    public interface IAddNewExpense
    {
        Task Execute(AddNewExpenseCommand command);
    }
}