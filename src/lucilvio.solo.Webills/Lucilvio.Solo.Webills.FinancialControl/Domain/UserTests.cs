using System;
using System.Linq;
using NUnit.Framework;

namespace Lucilvio.Solo.Webills.FinancialControl.Domain
{
    [TestFixture]
    internal class UserTests
    {
        [Test]
        public void CantCreateUserWithoutId()
        {
            Assert.Throws<User.Error.CantCreateUserWithoutId>(() =>
            {
                new User(Guid.Empty);
            });
        }

        [Test]
        public void CanAddIncomes()
        {
            var user = this.ValidUser;
            user.AddIncome("Test income", "salary", DateTime.Now, new TransactionValue(432));

            Assert.AreEqual(1, user.Incomes.Count());
        }

        [Test]
        public void DontRemoveExpenseIfCantFindIt()
        {
            Assert.Throws<User.Error.ExpenseNotFound>(() =>
            {
                this.ValidUser.RemoveExpense(Guid.NewGuid());
            });
        }

        [Test]
        public void DontRemoveIncomeIfCantFindIt()
        {
            Assert.Throws<User.Error.IncomeNotFound>(() =>
            {
                this.ValidUser.RemoveIncome(Guid.NewGuid());
            });
        }

        private User ValidUser => new User(Guid.NewGuid());
        private DateTime TestDateFrom => new DateTime(2000, 1, 1);
    }
}