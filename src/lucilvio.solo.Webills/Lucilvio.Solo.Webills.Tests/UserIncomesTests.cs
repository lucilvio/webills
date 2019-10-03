using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lucilvio.Solo.Webills.Tests
{
    [TestClass]
    public class UserIncomesTests
    {
        private User _user;

        [TestInitialize]
        public void Setup()
        {
            this._user = new User();
        }

        [TestMethod]
        public void UserHasIncomes()
        {
            Assert.IsNotNull(this._user.Incomes);
        }

        [TestMethod]
        public void UserCanRegisterIncome()
        {
            this._user.AddIncome(new Income("Test income", new DateTime(2018, 10, 23), new TransactionValue(300)));

            Assert.IsNotNull(this._user.Incomes);
            Assert.IsTrue(this._user.HasIncomes);
        }

        [TestMethod]
        [ExpectedException(typeof(UserCannotAddNullIncome))]
        public void UserCantRegisterNullIncome()
        {
            this._user.AddIncome(null);
        }

        [TestMethod]
        [ExpectedException(typeof(IncomeTransactionValueCannotBeNull))]
        public void UserCannorAddNullIncomeValue()
        {
            this._user.AddIncome(new Income("Test income", new DateTime(2018, 10, 23), null));
        }

        [TestMethod]
        [ExpectedException(typeof(TransactionValueCannotBeNegative))]
        public void UserCannotAddNegativeIncomeValues()
        {
            this._user.AddIncome(new Income("Test income", new DateTime(2018, 10, 23), new TransactionValue(-100)));
        }
    }
}