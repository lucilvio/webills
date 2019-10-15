using System;
using Lucilvio.Solo.Webills.Domain.User;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lucilvio.Solo.Webills.Domain.User.BusinessErrors;
using System.Linq;

namespace Lucilvio.Solo.Webills.Tests
{
    [TestClass]
    public class UserExpensesTests
    {
        private User _user;

        [TestInitialize]
        public void Init()
        {
            this._user = new User("Tests User");
        }

        [TestMethod]
        public void UserHasExpenses()
        {
            Assert.IsNotNull(this._user.Expenses);
        }

        [TestMethod]
        public void UserCanRegisterExpense()
        {
            this._user.AddExpense(new Expense("Test Expense", new DateTime(2018, 10, 23), new TransactionValue(300)));

            Assert.IsNotNull(this._user.Expenses);
            Assert.IsTrue(this._user.HasExpenses);
        }

        [TestMethod]
        [ExpectedException(typeof(UserCannotAddNullExpense))]
        public void UserCantRegisterNullExpense()
        {
            this._user.AddExpense(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ExpenseTransactionValueCannotBeNull))]
        public void UserCannorAddNullExpenceValue()
        {
            this._user.AddExpense(new Expense("Test expense", new DateTime(2018, 10, 23), null));
        }

        [TestMethod]
        [ExpectedException(typeof(TransactionValueCannotBeNegative))]
        public void UserCannotAddNegativeExpenseValue()
        {
            this._user.AddExpense(new Expense("Test Expense", new DateTime(2018, 10, 23), new TransactionValue(-200)));
        }

        [TestMethod]
        [ExpectedException(typeof(ExpenseNotFound))]
        public void ThrowsExpenseNotFoundWhenUserUserTrysToAlterAnExpenseThatDoesntExist()
        {
            var newExpenseNumber = this._user.AddExpense(new Expense("Edited", new DateTime(2019, 10, 20), new TransactionValue(30)));
            this._user.AlterExpense(Guid.NewGuid(), new Expense("Edited Expense", new DateTime(2018, 10, 20), new TransactionValue(303m)));
        }

        [TestMethod]
        public void UserCanAlterExpense()
        {
            var newExpenseNumber = this._user.AddExpense(new Expense("Edited", new DateTime(2019, 10, 20), new TransactionValue(30)));
            this._user.AlterExpense(newExpenseNumber, new Expense("Edited Expense", new DateTime(2018, 10, 20), new TransactionValue(303m)));

            Assert.AreEqual("Edited Expense", this._user.Expenses.First().Name);
            Assert.AreEqual(new DateTime(2018, 10, 20), this._user.Expenses.First().Date);
            Assert.AreEqual(new TransactionValue(303m), this._user.Expenses.First().Value);
        }

    }
}