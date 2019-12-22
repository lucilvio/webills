using System;
using System.Linq;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.Domain.User;
using Lucilvio.Solo.Webills.UseCases.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lucilvio.Solo.Webills.Tests.UseCases.AddNewExpense
{
    [TestClass]
    public class AddNewExpenseTests
    {
        [TestMethod]
        [ExpectedException(typeof(CommandNotInformed))]
        public async Task ThrowsCommandNotInformedWhenCommandIsNull()
        {
            var addNewExpense = new Webills.UseCases.AddNewExpense.AddNewExpense(new AddNewExpenseDataStorageStubWithTestUser());
            await addNewExpense.Execute(null);
        }

        [TestMethod]
        [ExpectedException(typeof(UserNotFound))]
        public async Task ThrowsUserNotFoundExceptionWhenTheUserIsNotFound()
        {
            var addNewExpense = new Webills.UseCases.AddNewExpense.AddNewExpense(new AddNewExpenseDataStorageStubWithoutUser());
            await addNewExpense.Execute(new AddNewExpenseCommandStub("Test expense", Category.Others, new DateTime(2019, 03, 01), TransactionValue.Zero));
        }

        [TestMethod]
        public async Task UserAddNewExpense()
        {
            var dataStorage = new AddNewExpenseDataStorageStubWithTestUser();
            var addNewExpense = new Webills.UseCases.AddNewExpense.AddNewExpense(dataStorage);
            await addNewExpense.Execute(new AddNewExpenseCommandStub("Test expense", Category.Taxes, new DateTime(2019, 03, 01), TransactionValue.Zero));

            var user = await dataStorage.GetUser();
            var addedExpense = user.Expenses.First();

            Assert.AreEqual("Test expense", addedExpense.Name);
            Assert.AreEqual(Category.Taxes, addedExpense.Category);
            Assert.AreEqual(new DateTime(2019, 03, 01), addedExpense.Date);
            Assert.AreEqual(TransactionValue.Zero, addedExpense.Value);
        }
    }
}
