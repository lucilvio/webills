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
            var user = this.ValidUser;
            user.AddExpense("Test expense", Category.BarAndRestaurant, DateTime.Now, new TransactionValue(432));

            Assert.AreEqual(1, user.Expenses.Count());
        }

        [Test]
        public void CanAddIncomes()
        {
            var user = this.ValidUser;
            user.AddIncome("Test income", DateTime.Now, new TransactionValue(432));

            Assert.AreEqual(1, user.Incomes.Count());
        }

        [Test]
        public void CanAddRecurrentExpenses()
        {
            var user = this.ValidUser;
            user.AddRecurrentExpense("Test expense", Category.Education, DateTime.Now, new TransactionValue(385),
                new Recurrency(1, Frequency.Days, DateTime.Now.AddYears(2)));

            Assert.AreEqual(1, user.RecurrentExpenses.Count());
            Assert.NotNull(user.RecurrentExpenses.First().Recurrency);
        }

        [Test]
        public void AddDailyRecurrencyExpensesForTwoWeeks()
        {
            DateTime date = this.TestDateFrom;

            var user = this.ValidUser;
            user.AddRecurrentExpense("Test expense", Category.Education, date, new TransactionValue(385),
                new Recurrency(1, Frequency.Days, date.AddDays(14)));

            Assert.AreEqual(15, user.RecurrentExpenses.First().ExpensesCount);
        }


        [Test]
        public void AddMonthlyRecurrencyExpensesForSevenMonths()
        {
            var date = this.TestDateFrom;

            var user = this.ValidUser;
            user.AddRecurrentExpense("Test expense", Category.Education, date, new TransactionValue(385),
                new Recurrency(1, Frequency.Months, date.AddMonths(7)));

            Assert.AreEqual(8, user.RecurrentExpenses.First().ExpensesCount);
        }

        [Test]
        public void AddTrimonthlyRecurrencyExpensesForSevenMonths()
        {
            var date = this.TestDateFrom;

            var user = this.ValidUser;
            user.AddRecurrentExpense("Test expense", Category.Education, date, new TransactionValue(385),
                new Recurrency(3, Frequency.Months, date.AddYears(1)));

            Assert.AreEqual(5, user.RecurrentExpenses.First().ExpensesCount);
        }

        [Test]
        public void AddAnnualRecurrencyExpensesForFiveYears()
        {
            var date = new DateTime(2000, 1, 1);

            var user = this.ValidUser;
            user.AddRecurrentExpense("Test expense", Category.Education, date, new TransactionValue(385),
                new Recurrency(1, Frequency.Years, date.AddYears(5)));

            Assert.AreEqual(6, user.RecurrentExpenses.First().ExpensesCount);
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
                ValidUser.RemoveIncome(Guid.NewGuid());
            });
        }

        private User ValidUser => new User(Guid.NewGuid());
        private DateTime TestDateFrom => new DateTime(2000, 1, 1);
    }
}