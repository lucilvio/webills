using System.Threading.Tasks;
using Lucilvio.Solo.Webills.Domain.User;
using Lucilvio.Solo.Webills.UseCases.Common;
using Lucilvio.Solo.Webills.UseCases.RemoveExpense;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lucilvio.Solo.Webills.UseCases.Contracts.RemoveExpense;
using System;
using System.Linq;

namespace Lucilvio.Solo.Webills.Tests.UseCases.RemoveExpense
{
    [TestClass]
    public class RemoveExpenseTest
    {
        [TestMethod]
        [ExpectedException(typeof(CommandNotInformed))]
        public async Task ThrowsCommandNotInformedIfCommandIsNull()
        {
            await new Webills.UseCases.RemoveExpense.RemoveExpense(new RemoveExpenseDataStorageStubWithoutUser()).Execute(null);
        }

        [TestMethod]
        [ExpectedException(typeof(UserNotFound))]
        public async Task ThrowsUserNotFoundIfAnyUserWasFound()
        {
            await new Webills.UseCases.RemoveExpense.RemoveExpense(new RemoveExpenseDataStorageStubWithoutUser()).Execute(new RemoveExpenseCommandStub(Guid.Empty));
        }

        [TestMethod]
        public async Task RemoveExpense()
        {
            var dataStorage = new RemoveExpenseDataStorageStubWithOneUserWithExpense();
            var useCase = new Webills.UseCases.RemoveExpense.RemoveExpense(dataStorage);

            var user = await dataStorage.GetUser();

            await useCase.Execute(new RemoveExpenseCommandStub(user.Expenses.First().Number));


            Assert.AreEqual(0, user.Expenses.Count());
        }
    }

    internal class RemoveExpenseDataStorageStubWithOneUserWithExpense : IRemoveExpenseDataStorage
    {
        private User _user;

        public RemoveExpenseDataStorageStubWithOneUserWithExpense()
        {
            this._user = new User("test user");
            this._user.AddExpense("Test income", Category.Others, DateTime.Now, TransactionValue.Zero);
        }

        public async Task<User> GetUser()
        {
            return this._user;
        }

        public async Task Persist()
        {
        }
    }

    internal class RemoveExpenseCommandStub : RemoveExpenseCommand
    {
        public RemoveExpenseCommandStub(Guid expenseNumber)
        {
            base.ExpenseNumber = expenseNumber;
        }
    }

    internal class RemoveExpenseDataStorageStubWithoutUser : IRemoveExpenseDataStorage
    {
        public async Task<User> GetUser()
        {
            return null;
        }

        public async Task Persist()
        {
            return;
        }
    }
}
