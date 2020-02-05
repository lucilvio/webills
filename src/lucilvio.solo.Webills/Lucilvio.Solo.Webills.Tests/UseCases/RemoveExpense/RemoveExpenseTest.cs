using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Core.Domain.User;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lucilvio.Solo.Webills.Core.UseCases.RemoveExpense;
using Lucilvio.Solo.Webills.Core.UseCases.Contracts.RemoveExpense;
using Lucilvio.Solo.Webills.Shared.UseCases.Errors;

namespace Lucilvio.Solo.Webills.Tests.UseCases.RemoveExpense
{
    [TestClass]
    public class RemoveExpenseTest
    {
        [TestMethod]
        [ExpectedException(typeof(CommandNotInformed))]
        public async Task ThrowsCommandNotInformedIfCommandIsNull()
        {
            var dataStorageWithoutBehavior = new Mock<IRemoveExpenseDataStorage>().Object;

            await new Core.UseCases.RemoveExpense.RemoveExpense(dataStorageWithoutBehavior).Execute(null);
        }

        [TestMethod]
        [ExpectedException(typeof(UserNotFound))]
        public async Task ThrowsUserNotFoundIfAnyUserWasFound()
        {
            var dataStorageWithoutUser = new Mock<IRemoveExpenseDataStorage>();
            dataStorageWithoutUser.Setup(obj => obj.GetUserById(It.IsAny<Guid>())).ReturnsAsync(null as User);

            await new Core.UseCases.RemoveExpense.RemoveExpense(dataStorageWithoutUser.Object).Execute(new RemoveExpenseCommandStub(Guid.Empty, Guid.Empty));
        }

        [TestMethod]
        public async Task RemoveExpense()
        {
            var user = new User("Sample user");
            var expenseId = user.AddExpense("Sample expense", Category.Clothing, DateTime.Now, TransactionValue.Zero);

            var dataStorageWithUser = new Mock<IRemoveExpenseDataStorage>();
            dataStorageWithUser.Setup(obj => obj.GetUserById(user.Id)).ReturnsAsync(user);

            await new Core.UseCases.RemoveExpense.RemoveExpense(dataStorageWithUser.Object).Execute(new RemoveExpenseCommandStub(user.Id, expenseId));

            Assert.AreEqual(0, user.Expenses.Count());
        }
    }

    internal class RemoveExpenseCommandStub : RemoveExpenseCommand
    {
        public RemoveExpenseCommandStub(Guid userId, Guid expenseId) : base(userId, expenseId)
        {
        }
    }
}
