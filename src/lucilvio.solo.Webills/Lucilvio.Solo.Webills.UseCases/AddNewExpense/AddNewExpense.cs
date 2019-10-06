using Lucilvio.Solo.Webills.Domain.User;
using Lucilvio.Solo.Webills.UseCases.AddNewIncome;

namespace Lucilvio.Solo.Webills.UseCases.AddNewExpense
{
    public class AddNewExpense : IAddNewExpense
    {
        private readonly IAddNewExpenseDataStorage _dataStorage;

        public AddNewExpense(IAddNewExpenseDataStorage dataStorage)
        {
            this._dataStorage = dataStorage;
        }

        public void Execute(AddNewExpenseCommand command)
        {
            if (command == null)
                throw new CommandNotInformed();

            var foundUser = this._dataStorage.GetUser();

            if (foundUser == null)
                throw new UserNotFound();

            foundUser.AddExpense(new Expense(command.Name, command.Date, command.Value));

            this._dataStorage.Persist(foundUser);
        }
    }
}