using Lucilvio.Solo.Webills.Domain.User;

namespace Lucilvio.Solo.Webills.UseCases.AddNewExpense
{
    public interface IAddNewExpenseDataStorage
    {
        User GetUser();
        void Persist(User user);
    }
}