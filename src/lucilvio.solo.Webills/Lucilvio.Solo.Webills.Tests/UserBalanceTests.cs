using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lucilvio.Solo.Webills.Tests
{
    [TestClass]
    public class UserBalanceTests
    {
        [TestMethod]
        public void UserHasBalance()
        {
            var user = new User();
            Assert.IsNotNull(user.Balance);
        }

        [TestMethod]
        public void BalanceEqualsTo300IfUserAddsIncomeValueOf300()
        {
            var user = new User();
            user.AddIncome(new Income("Test Income", new DateTime(2018, 10, 23), new TransactionValue(300)));

            Assert.AreEqual(300, user.Balance);
        }

        [TestMethod]
        public void BalanceEqualsToIncomesAddedValues()
        {
            var user = new User();
            user.AddIncome(new Income("Test Income", new DateTime(2018, 10, 23), new TransactionValue(300)));
            user.AddIncome(new Income("Test Income", new DateTime(2018, 10, 23), new TransactionValue(200)));
            user.AddIncome(new Income("Test Income", new DateTime(2018, 10, 23), new TransactionValue(35)));

            Assert.AreEqual(535, user.Balance);
        }

        [TestMethod]
        public void BalanceEqualsToIncomeAddedValuesAndSubtractedByExpansesValues()
        {
            var user = new User();
            user.AddIncome(new Income("Test Income", new DateTime(2018, 10, 23), new TransactionValue(300)));
            user.AddIncome(new Income("Test Income", new DateTime(2018, 10, 23), new TransactionValue(200)));
            user.AddExpanse(new Expanse("Test Expanse", new DateTime(2018, 10, 23), new TransactionValue(24)));

            Assert.AreEqual(476, user.Balance);

        }
    }
}