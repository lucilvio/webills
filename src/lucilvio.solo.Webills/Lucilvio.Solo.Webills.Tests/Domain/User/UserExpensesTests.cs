using System;
using System.Linq;
using Lucilvio.Solo.Webills.Domain.User;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lucilvio.Solo.Webills.Domain.User.BusinessErrors;

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
            this._user.AddExpense("Test Expense", Category.Taxes, new DateTime(2018, 10, 23), new TransactionValue(300));

            Assert.IsNotNull(this._user.Expenses);
            Assert.IsTrue(this._user.HasExpenses);

            Assert.AreEqual("Test Expense", this._user.Expenses.First().Name);
            Assert.AreEqual(new DateTime(2018, 10, 23), this._user.Expenses.First().Date);
            Assert.AreEqual(Category.Taxes, this._user.Expenses.First().Category);
            Assert.AreEqual(new TransactionValue(300), this._user.Expenses.First().Value);
        }

        [TestMethod]
        [ExpectedException(typeof(ExpenseMustHaveName))]
        public void UserCannotAddNamelessExpense()
        {
            this._user.AddExpense(null, Category.Others, DateTime.MinValue, TransactionValue.Zero);
        }

        [TestMethod]
        [ExpectedException(typeof(ExpenseTransactionValueCannotBeNull))]
        public void UserCannotAddNullExpenceValue()
        {
            this._user.AddExpense("Test expense", Category.Others, new DateTime(2018, 10, 23), null);
        }

        [TestMethod]
        [ExpectedException(typeof(TransactionValueCannotBeNegative))]
        public void UserCannotAddNegativeExpenseValue()
        {
            this._user.AddExpense("Test Expense", Category.Others, new DateTime(2018, 10, 23), new TransactionValue(-200));
        }

        [TestMethod]
        public void UserExepenseHasCategory()
        {
            this._user.AddExpense("Test Expense", Category.Others, DateTime.Now, new TransactionValue(200));
            Assert.IsNotNull(this._user.Expenses.First().Category);
        }

        [TestMethod]
        [ExpectedException(typeof(ExpenseNotFound))]
        public void ThrowsExpenseNotFoundWhenUserUserTrysToAlterAnExpenseThatDoesntExist()
        {
            this._user.AddExpense("Edited", Category.Others, new DateTime(2019, 10, 20), new TransactionValue(30));
            this._user.AlterExpense(Guid.NewGuid(), "Edited Expense", Category.Others, new DateTime(2018, 10, 20), new TransactionValue(303m));
        }

        [TestMethod]
        public void UserCanAlterExpense()
        {
            var newExpenseNumber = this._user.AddExpense("Edited", Category.Others, new DateTime(2019, 10, 20), new TransactionValue(30));
            this._user.AlterExpense(newExpenseNumber, "Edited Expense", Category.Others, new DateTime(2018, 10, 20), new TransactionValue(303m));

            Assert.AreEqual("Edited Expense", this._user.Expenses.First().Name);
            Assert.AreEqual(new DateTime(2018, 10, 20), this._user.Expenses.First().Date);
            Assert.AreEqual(new TransactionValue(303m), this._user.Expenses.First().Value);
        }

        [TestMethod]
        public void TotalSpentOfTheUserIsTheSumOfAllExpenses()
        {
            this._user.AddExpense("Test expense", Category.Others, DateTime.Now, new TransactionValue(342.12m));
            this._user.AddExpense("Test expense", Category.Others, DateTime.Now, new TransactionValue(300.12m));
            this._user.AddExpense("Test expense", Category.Others, DateTime.Now, new TransactionValue(1000.12m));
            this._user.AddExpense("Test expense", Category.Others, DateTime.Now, new TransactionValue(1.12m));

            Assert.AreEqual(1643.48m, this._user.TotalExpenses);
        }

        [TestMethod]
        public void UserCanRemoveExpense()
        {
            var expenseNumber1 = this._user.AddExpense("Test expense", Category.Others, DateTime.Now, new TransactionValue(200.90m));
            var expenseNumber2 = this._user.AddExpense("Test expense", Category.Others, DateTime.Now, new TransactionValue(200.90m));
            this._user.RemoveExpense(expenseNumber1);

            Assert.AreEqual(1, this._user.Expenses.Count());
            Assert.AreEqual(expenseNumber2, this._user.Expenses.First().Number);
        }

        [TestMethod]
        [ExpectedException(typeof(ExpenseNotFound))]
        public void ThrowsExpenseNotFoundWhenUserTriesToRemoveAnInexistentExpense()
        {
            this._user.AddExpense("Test expense", Category.Others, DateTime.Now, new TransactionValue(200.90m));
            this._user.AddExpense("Test expense", Category.Others, DateTime.Now, new TransactionValue(200.90m));
            
            this._user.RemoveExpense(Guid.Empty);
        }
    }
}