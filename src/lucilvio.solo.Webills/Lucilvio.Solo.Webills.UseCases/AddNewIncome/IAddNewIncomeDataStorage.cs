using Lucilvio.Solo.Webills.Domain.User;

namespace Lucilvio.Solo.Webills.UseCases.AddNewIncome
{
    public interface IAddNewIncomeDataStorage
    {
        User GetUser();
        void Persist(User user);
    }
}