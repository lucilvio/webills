using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Lucilvio.Solo.Webills.Core.Domain.User;
using Lucilvio.Solo.Webills.UseCases.Shared.Errors;
using Lucilvio.Solo.Webills.Core.UseCases.AddNewExpense;
using Lucilvio.Solo.Webills.Core.UseCases.Contracts.AddNewExpense;

namespace Lucilvio.Solo.Webills.Tests.UseCases.AddNewExpense
{
    [TestClass]
    public class AddNewExpenseTests
    {
        [TestMethod]
        [ExpectedException(typeof(CommandNotInformed))]
        public async Task ThrowsCommandNotInformedWhenCommandIsNull()
        {
            var dataStorageWithoutBehavior = new Mock<IAddNewExpenseDataStorage>();

            var addNewExpense = new Core.UseCases.AddNewExpense.AddNewExpense(dataStorageWithoutBehavior.Object);
            await addNewExpense.Execute(null);
        }

        [TestMethod]
        [ExpectedException(typeof(UserNotFound))]
        public async Task ThrowsUserNotFoundExceptionWhenTheUserIsNotFound()
        {
            var dataStorageWithoutUser = new Mock<IAddNewExpenseDataStorage>();

            var addNewExpense = new Core.UseCases.AddNewExpense.AddNewExpense(dataStorageWithoutUser.Object);
            await addNewExpense.Execute(new AddNewExpenseCommandMock(Guid.NewGuid(), "Test expense", Category.Others, new DateTime(2019, 03, 01), TransactionValue.Zero));
        }

        [TestMethod]
        public async Task UserAddNewExpense()
        {
            var userId = Guid.NewGuid();
            var user = new User("sample user");

            var dataStorageWithUser = new Mock<IAddNewExpenseDataStorage>();
            dataStorageWithUser.Setup(obj => obj.GetUserById(userId)).ReturnsAsync(user);

            var addNewExpense = new Core.UseCases.AddNewExpense.AddNewExpense(dataStorageWithUser.Object);
            await addNewExpense.Execute(new AddNewExpenseCommandMock(userId, "Test expense", Category.Taxes, new DateTime(2019, 03, 01), TransactionValue.Zero));

            Assert.IsTrue(user.Expenses.Count() == 1);
        }
    }

    internal class AddNewExpenseCommandMock : AddNewExpenseCommand
    {
        public AddNewExpenseCommandMock(Guid userId, string name, Category category, DateTime date, TransactionValue value)
        {
            base.UserId = userId;
            base.Name = name;
            base.Category = category;
            base.Date = date;
            base.Value = value;
        }
    }
}
