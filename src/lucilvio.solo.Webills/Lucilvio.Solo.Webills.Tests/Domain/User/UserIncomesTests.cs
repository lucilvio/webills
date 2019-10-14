using System;
using Lucilvio.Solo.Webills.Domain.User;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lucilvio.Solo.Webills.Domain.User.BusinessErrors;
using System.Linq;

namespace Lucilvio.Solo.Webills.Tests
{
    [TestClass]
    public class UserIncomesTests
    {
        private User _user;

        [TestInitialize]
        public void Setup()
        {
            this._user = new User("Test User");
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
        [ExpectedException(typeof(IncomeMustHaveName))]
        public void UserCannotAddNamelessIncome()
        {
            this._user.AddIncome(new Income("", new DateTime(2018, 10, 23), new TransactionValue(43)));
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

        [TestMethod]
        public void UserIncomeHasNumber()
        {
            this._user.AddIncome(new Income("Test income", new DateTime(2018, 10, 23), TransactionValue.Zero));

            Assert.IsNotNull(this._user.Incomes.First().Number);
            Assert.AreNotEqual(Guid.Empty, this._user.Incomes.First().Number);
        }

        [TestMethod]
        [ExpectedException(typeof(IncomeNotFound))]
        public void ThrowsIncomeNotFoundWhenUserTrysToAlterAnIncomeThatDoesntExist()
        {
            this._user.AddIncome(new Income("Salary", DateTime.Now, new TransactionValue(2675.89m)));
            this._user.AlterIncome(Guid.NewGuid(), new Income("Salary2", new DateTime(2019, 1, 20), new TransactionValue(2600.80m)));
        }

        [TestMethod]
        public void UserCanAlterIncome()
        {
            var newIncome = this._user.AddIncome(new Income("Salary", new DateTime(2019, 1, 20), new TransactionValue(3908.3m)));
            this._user.AlterIncome(newIncome, new Income("Salary edited", new DateTime(2019, 3, 3), new TransactionValue(20m)));

            Assert.AreEqual("Salary edited", this._user.Incomes.First().Name);
            Assert.AreEqual(new DateTime(2019, 3, 3), this._user.Incomes.First().Date);
            Assert.AreEqual(new TransactionValue(20m), this._user.Incomes.First().Value);
        }
    }
}