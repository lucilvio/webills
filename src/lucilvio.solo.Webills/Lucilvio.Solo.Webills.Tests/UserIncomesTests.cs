using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lucilvio.Solo.Webills.Tests
{
    [TestClass]
    public class UserIncomesTests
    {
        [TestMethod]
        public void UserHasIncomes()
        {
            var user = new User();
            Assert.IsNotNull(user.Incomes);
        }

        [TestMethod]
        public void UserCanRegisterIncome()
        {
            var user = new User();
            user.AddIncome(new Income("Test income", new DateTime(2018, 10, 23), new TransactionValue(300)));

            Assert.IsNotNull(user.Incomes);
            Assert.IsTrue(user.HasIncomes);
        }

        [TestMethod]
        [ExpectedException(typeof(UserCannotAddNullIncome))]
        public void UserCantRegisterNullIncome()
        {
            var user = new User();
            user.AddIncome(null);
        }

        [TestMethod]
        [ExpectedException(typeof(IncomeTransactionValueCannotBeNull))]
        public void UserCannorAddNullIncomeValue()
        {
            var user = new User();
            user.AddIncome(new Income("Test income", new DateTime(2018, 10, 23), null));
        }

        [TestMethod]
        [ExpectedException(typeof(TransactionValueCannotBeNegative))]
        public void UserCannotAddNegativeIncomeValues()
        {
            var user = new User();
            user.AddIncome(new Income("Test income", new DateTime(2018, 10, 23), new TransactionValue(-100)));
        }
    }
}