using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lucilvio.Solo.Webills.Tests
{
    [TestClass]
    public class UserExpensesTests
    {
        [TestMethod]
        public void UserHasExpenses()
        {
            var user = new User();
            Assert.IsNotNull(user.Expenses);
        }

        [TestMethod]
        public void UserCanRegisterExpense()
        {
            var user = new User();
            user.AddExpense(new Expense("Test Expense", new DateTime(2018, 10, 23), new TransactionValue(300)));

            Assert.IsNotNull(user.Expenses);
            Assert.IsTrue(user.HasExpenses);
        }

        [TestMethod]
        [ExpectedException(typeof(UserCannotAddNullExpense))]
        public void UserCantRegisterNullExpense()
        {
            var user = new User();
            user.AddExpense(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ExpenseTransactionValueCannotBeNull))]
        public void UserCannorAddNullExpenceValue()
        {
            var user = new User();
            user.AddExpense(new Expense("Test expense", new DateTime(2018, 10, 23), null));
        }

        [TestMethod]
        [ExpectedException(typeof(TransactionValueCannotBeNegative))]
        public void UserCannotAddNegativeExpenseValue()
        {
            var user = new User();
            user.AddExpense(new Expense("Test Expense", new DateTime(2018, 10, 23), new TransactionValue(-200)));
        }
    }
}