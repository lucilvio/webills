namespace Lucilvio.Solo.Webills.UseCases.AddNewExpense
{
    public interface IAddNewExpense
    {
        void Execute(AddNewExpenseCommand command);
    }
}