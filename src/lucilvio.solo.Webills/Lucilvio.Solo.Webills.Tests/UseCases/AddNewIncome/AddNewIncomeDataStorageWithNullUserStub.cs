using Lucilvio.Solo.Webills.Domain.User;

namespace Lucilvio.Solo.Webills.UseCases.AddNewIncome
{
    public class AddNewIncomeDataStorageWithNullUserStub : IAddNewIncomeDataStorage
    {
        public User GetUser()
        {
            return null;
        }

        public void Persist(User user)
        {
        }
    }
}