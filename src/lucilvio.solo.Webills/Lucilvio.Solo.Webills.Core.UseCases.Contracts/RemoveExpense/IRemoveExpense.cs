using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.Core.UseCases.Contracts.RemoveExpense
{
    public interface IRemoveExpense
    {
        Task Execute(RemoveExpenseCommand removeExpenseCommand);
    }
}