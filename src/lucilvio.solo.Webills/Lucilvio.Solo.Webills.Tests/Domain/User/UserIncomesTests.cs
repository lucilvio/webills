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
            this._user = new User("Test User", new Login("user@mail.com"), new Password("123456"));
        }

        [TestMethod]
        public void UserHasIncomes()
        {
            Assert.IsNotNull(this._user.Incomes);
        }

        [TestMethod]
        public void UserCanRegisterIncome()
        {
            this._user.AddIncome("Test income", new DateTime(2018, 10, 23), new TransactionValue(300));

            Assert.IsNotNull(this._user.Incomes);
            Assert.IsTrue(this._user.Incomes.Any());
        }

        [TestMethod]
        [ExpectedException(typeof(IncomeMustHaveName))]
        public void UserCannotAddNamelessIncome()
        {
            this._user.AddIncome("", new DateTime(2018, 10, 23), new TransactionValue(43));
        }

        [TestMethod]
        [ExpectedException(typeof(IncomeTransactionValueCannotBeNull))]
        public void UserCannorAddNullIncomeValue()
        {
            this._user.AddIncome("Test income", new DateTime(2018, 10, 23), null);
        }

        [TestMethod]
        [ExpectedException(typeof(TransactionValueCannotBeNegative))]
        public void UserCannotAddNegativeIncomeValues()
        {
            this._user.AddIncome("Test income", new DateTime(2018, 10, 23), new TransactionValue(-100));
        }

        [TestMethod]
        public void UserIncomeHasNumber()
        {
            this._user.AddIncome("Test income", new DateTime(2018, 10, 23), TransactionValue.Zero);

            Assert.IsNotNull(this._user.Incomes.First().Number);
            Assert.AreNotEqual(Guid.Empty, this._user.Incomes.First().Number);
        }

        [TestMethod]
        [ExpectedException(typeof(IncomeNotFound))]
        public void ThrowsIncomeNotFoundWhenUserTrysToAlterAnIncomeThatDoesntExist()
        {
            this._user.AddIncome("Salary", DateTime.Now, new TransactionValue(2675.89m));
            this._user.AlterIncome(Guid.NewGuid(), "Salary2", new DateTime(2019, 1, 20), new TransactionValue(2600.80m));
        }

        [TestMethod]
        public void UserCanAlterIncome()
        {
            var newIncome = this._user.AddIncome("Salary", new DateTime(2019, 1, 20), new TransactionValue(3908.3m));
            this._user.AlterIncome(newIncome, "Salary edited", new DateTime(2019, 3, 3), new TransactionValue(20m));

            Assert.AreEqual("Salary edited", this._user.Incomes.First().Name);
            Assert.AreEqual(new DateTime(2019, 3, 3), this._user.Incomes.First().Date);
            Assert.AreEqual(new TransactionValue(20m), this._user.Incomes.First().Value);
        }

        [TestMethod]
        public void TotalEarnsOfTheUserIsTheSumOfAllIncomes()
        {
            this._user.AddIncome("Test Income", DateTime.Now, new TransactionValue(30000.20m));
            this._user.AddIncome("Test Income", DateTime.Now, new TransactionValue(2131m));

            Assert.AreEqual(32131.20m, this._user.Incomes.Sum(i => i.Value.Value));
        }

        [TestMethod]
        [ExpectedException(typeof(IncomeNotFound))]
        public void ThrowsIncomeNotFoundWhenUserTriesToRemoveAnInexistentIncome()
        {
            this._user.AddIncome("Test Income", DateTime.Now, new TransactionValue(30000.20m));
            this._user.RemoveIncome(Guid.Empty);
        }

        [TestMethod]
        public void UserCanRemoveIncome()
        {
            var incomeNumber1 = this._user.AddIncome("Test Income", DateTime.Now, new TransactionValue(30000.20m));
            var incomeNumber2 = this._user.AddIncome("Test Income", DateTime.Now, new TransactionValue(30000.20m));
            
            this._user.RemoveIncome(incomeNumber1);

            Assert.AreEqual(1, this._user.Incomes.Count());
            Assert.AreEqual(incomeNumber2, this._user.Incomes.First().Number);
        }
    }
}