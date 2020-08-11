using System;
using System.Linq;

using NUnit.Framework;

namespace Lucilvio.Solo.Webills.Transactions.Domain
{
    [TestFixture]
    public class UserTests
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
        public void CanAddExpense()
        {
            var user = new User(Guid.NewGuid());
            user.AddExpense("Test expense", Category.BarAndRestaurant, DateTime.Now, new TransactionValue(432));

            Assert.AreEqual(1, user.Expenses.Count());
        }

        [Test]
        public void CanAddIncomes()
        {
            var user = new User(Guid.NewGuid());
            user.AddIncome("Test income", DateTime.Now, new TransactionValue(432));

            Assert.AreEqual(1, user.Incomes.Count());
        }

        [Test]
        public void DontRemoveExpenseIfCantFindIt()
        {
            var user = new User(Guid.NewGuid());
            
            Assert.Throws<User.Error.ExpenseNotFound>(() =>
            {
                user.RemoveExpense(Guid.NewGuid());
            });
        }

        [Test]
        public void DontRemoveIncomeIfCantFindIt()
        {
            var user = new User(Guid.NewGuid());

            Assert.Throws<User.Error.IncomeNotFound>(() =>
            {
                user.RemoveIncome(Guid.NewGuid());
            });
        }
    }
}
