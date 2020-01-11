﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.Core.Domain.User;
using Lucilvio.Solo.Webills.Core.UseCases.EditExpense;
using Lucilvio.Solo.Webills.UseCases.Shared.Errors;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lucilvio.Solo.Webills.Tests.UseCases.EditExpenses
{
    [TestClass]
    public class EditExpensesTests
    {
        [TestMethod]
        [ExpectedException(typeof(CommandNotInformed))]
        public async Task ThrowsCommandNotInformedIfCommandIsNull()
        {
            await new EditExpense(new EditExpenseDataStorageStubWithOneUserWithExpense()).Execute(null);
        }

        [TestMethod]
        [ExpectedException(typeof(UserNotFound))]
        public async Task ThrowsUserNotFoundIfUserWasNotFound()
        {
            await new EditExpense(new EditExpenseDataStorageStubWithoutUser()).Execute(new EditExpenseCommandStubEmpty());
        }

        [TestMethod]
        public async Task UserEditExpense()
        {
            var dataStorage = new EditExpenseDataStorageStubWithOneUserWithExpense();
            var user = await dataStorage.GetUser();

            var useCase = new EditExpense(dataStorage);
            await useCase.Execute(new EditExpenseCommandStub(user.Expenses.First().Id, "Test expense edited", Category.Clothing,
                new DateTime(2019, 2, 20), new TransactionValue(320.89m)));

            Assert.AreEqual("Test expense edited", user.Expenses.First().Name);
            Assert.AreEqual(Category.Clothing, user.Expenses.First().Category);
            Assert.AreEqual(new DateTime(2019, 2, 20), user.Expenses.First().Date);
            Assert.AreEqual(new TransactionValue(320.89m), user.Expenses.First().Value);
        }
    }
}
