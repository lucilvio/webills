using System;
using Lucilvio.Solo.Webills.Domain.User;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lucilvio.Solo.Webills.Tests
{
    [TestClass]
    public class UserBalanceTests
    {
        private User _user;

        [TestInitialize]
        public void Setup()
        {
            this._user = new User("Test User");
        }

        [TestMethod]
        public void UserHasBalance()
        {
            Assert.IsNotNull(this._user.Balance);
        }

        [TestMethod]
        public void BalanceEqualsTo300IfUserAddsIncomeValueOf300()
        {
            this._user.AddIncome(new Income("Test Income", new DateTime(2018, 10, 23), new TransactionValue(300)));

            Assert.AreEqual(300, this._user.Balance);
        }

        [TestMethod]
        public void BalanceEqualsToIncomesAddedValues()
        {
            this._user.AddIncome(new Income("Test Income", new DateTime(2018, 10, 23), new TransactionValue(300)));
            this._user.AddIncome(new Income("Test Income", new DateTime(2018, 10, 23), new TransactionValue(200)));
            this._user.AddIncome(new Income("Test Income", new DateTime(2018, 10, 23), new TransactionValue(35)));

            Assert.AreEqual(535, this._user.Balance);
        }

        [TestMethod]
        public void BalanceEqualsToIncomeAddedValuesAndSubtractedByExpensesValues()
        {
            this._user.AddIncome(new Income("Test Income", new DateTime(2018, 10, 23), new TransactionValue(300)));
            this._user.AddIncome(new Income("Test Income", new DateTime(2018, 10, 23), new TransactionValue(200)));
            this._user.AddExpense(new Expense("Test Expense", new DateTime(2018, 10, 23), new TransactionValue(24)));

            Assert.AreEqual(476, this._user.Balance);

        }
    }
}