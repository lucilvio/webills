using Lucilvio.Solo.Webills.Domain.User;

namespace Lucilvio.Solo.Webills.UseCases.AddNewIncome
{
    public class AddNewIncomeDataStorageWithTestUserStub : IAddNewIncomeDataStorage
    {
        public User GetUser()
        {
            return new User("Test User");
        }

        public void Persist(User user)
        {
        }
    }
}