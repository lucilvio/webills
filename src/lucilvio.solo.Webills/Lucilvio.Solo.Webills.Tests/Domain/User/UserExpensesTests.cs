using System;
using System.Linq;
using Lucilvio.Solo.Webills.Domain.User;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lucilvio.Solo.Webills.Domain.User.BusinessErrors;

namespace Lucilvio.Solo.Webills.Tests
{
    [TestClass]
    public class UserExpensesTests
    {
        private User _user;

        [TestInitialize]
        public void Init()
        {
            this._user = new User("Tests User", new Login("user@mail.com"), new Password("123456"));
        }

        [TestMethod]
        public void UserHasExpenses()
        {
            Assert.IsNotNull(this._user.Expenses);
        }

        [TestMethod]
        public void UserCanRegisterExpense()
        {
            this._user.AddExpense("Test Expense", Category.Taxes, new DateTime(2018, 10, 23), new TransactionValue(300));

            Assert.IsNotNull(this._user.Expenses);
            Assert.IsTrue(this._user.Expenses.Any());

            Assert.AreEqual("Test Expense", this._user.Expenses.First().Name);
            Assert.AreEqual(new DateTime(2018, 10, 23), this._user.Expenses.First().Date);
            Assert.AreEqual(Category.Taxes, this._user.Expenses.First().Category);
            Assert.AreEqual(new TransactionValue(300), this._user.Expenses.First().Value);
        }

        [TestMethod]
        public void UserCanRegisterFixedExpense()
        {
            var until = new DateTime(2019, 10, 23);

            this._user.AddFixedExpense("Test fixed expense", Category.Taxes, new DateTime(2018, 10, 31), new TransactionValue(300),
                Recurrency.Monthly, until);

            Assert.IsNotNull(this._user.Expenses);
            Assert.IsTrue(this._user.Expenses.Any());

            Assert.AreEqual("Test fixed expense", this._user.Expenses.First().Name);
            Assert.AreEqual(new DateTime(2018, 10, 31), this._user.Expenses.First().Date);
            Assert.AreEqual(Category.Taxes, this._user.Expenses.First().Category);
            Assert.AreEqual(new TransactionValue(300), this._user.Expenses.First().Value);
            Assert.AreEqual(Recurrency.Monthly, ((FixedExpense)this._user.Expenses.First()).Recurrency);
            Assert.AreEqual(until, ((FixedExpense)this._user.Expenses.First()).Until);
        }

        [TestMethod]
        [ExpectedException(typeof(FixedExpenseLimitDateMustBeGreaterThenExpenseDate))]
        public void UserCannotRegisterFixedExpenseWithLimitDateLessThanExpenseDate()
        {
            this._user.AddFixedExpense("Test fixed expense", Category.Taxes, new DateTime(2018, 10, 23), new TransactionValue(300),
                Recurrency.Monthly, new DateTime(2018, 09, 20));
        }

        [TestMethod]
        public void WhenUserRegisterFixedExpenseWithDailyRecurrencyTheExpenseWillAppearUntilTheLimitDate()
        {
            this._user.AddFixedExpense("Test fixed expense", Category.Taxes, DateTime.Now, new TransactionValue(300),
                Recurrency.Daily, DateTime.Now.AddDays(20));
            
            Assert.AreEqual(21, this._user.Expenses.Count());
        }

        [TestMethod]
        public void WhenUserRegisterFixedExpenseWithWeekRecurrencyTheExpenseWillAppearUntilTheLimitDate()
        {
            var expenseDate = new DateTime(2019, 12, 1);
            var dayOfTheWeek = expenseDate.DayOfWeek;

            this._user.AddFixedExpense("Test fixed expense", Category.Taxes, expenseDate, new TransactionValue(300),
                Recurrency.Weekly, new DateTime(2020, 12, 1));

            Assert.AreEqual(53, this._user.Expenses.Count());

            foreach (var expense in this._user.Expenses)
            {
                Assert.AreEqual(dayOfTheWeek, expense.Date.DayOfWeek);
            }
        }

        [TestMethod]
        public void WhenUserRegisterFixedExpenseWithBiweekRecurrencyTheExpenseWillAppearUntilTheLimitDate()
        {
            var expenseDate = new DateTime(2019, 12, 1);
            var dayOfTheWeek = expenseDate.DayOfWeek;

            this._user.AddFixedExpense("Test fixed expense", Category.Taxes, expenseDate, new TransactionValue(300),
                Recurrency.Biweekly, new DateTime(2020, 12, 1));

            Assert.AreEqual(27, this._user.Expenses.Count());

            foreach (var expense in this._user.Expenses)
            {
                Assert.AreEqual(dayOfTheWeek, expense.Date.DayOfWeek);
            }
        }

        [TestMethod]
        public void WhenUserRegisterFixedExpenseWithMonthlyRecurrencyTheExpenseWillAppearUntilTheLimitDate()
        {
            var expenseDate = new DateTime(2019, 12, 1);

            this._user.AddFixedExpense("Test fixed expense", Category.Taxes, expenseDate, new TransactionValue(300),
                Recurrency.Monthly, DateTime.Now.AddDays(365));

            Assert.AreEqual(13, this._user.Expenses.Count());

            foreach (var expense in this._user.Expenses)
            {
                Assert.AreEqual(1, expense.Date.Day);
            }
        }

        [TestMethod]
        public void WhenUserRegisterFixedExpenseWithBimonthlyRecurrencyTheExpenseWillAppearUntilTheLimitDate()
        {
            var expenseDate = new DateTime(2019, 12, 1);

            this._user.AddFixedExpense("Test fixed expense", Category.Taxes, expenseDate, new TransactionValue(300),
                Recurrency.Bimonthly, DateTime.Now.AddMonths(3));

            Assert.AreEqual(2, this._user.Expenses.Count());

            foreach (var expense in this._user.Expenses)
            {
                Assert.AreEqual(1, expense.Date.Day);
            }
        }

        [TestMethod]
        public void WhenUserRegisterFixedExpenseWithTrimonthlyRecurrencyTheExpenseWillAppearUntilTheLimitDate()
        {
            var expenseDate = new DateTime(2019, 12, 1);

            this._user.AddFixedExpense("Test fixed expense", Category.Taxes, expenseDate, new TransactionValue(300),
                Recurrency.Trimonthly, DateTime.Now.AddMonths(5));

            Assert.AreEqual(2, this._user.Expenses.Count());

            foreach (var expense in this._user.Expenses)
            {
                Assert.AreEqual(1, expense.Date.Day);
            }
        }

        [TestMethod]
        public void WhenUserRegisterFixedExpenseWithQuarterlyRecurrencyTheExpenseWillAppearUntilTheLimitDate()
        {
            var expenseDate = new DateTime(2019, 12, 1);

            this._user.AddFixedExpense("Test fixed expense", Category.Taxes, expenseDate, new TransactionValue(300),
                Recurrency.Quarterly, DateTime.Now.AddMonths(9));

            Assert.AreEqual(3, this._user.Expenses.Count());

            foreach (var expense in this._user.Expenses)
            {
                Assert.AreEqual(1, expense.Date.Day);
            }
        }

        [TestMethod]
        public void WhenUserRegisterFixedExpenseWithSemiannualyRecurrencyTheExpenseWillAppearUntilTheLimitDate()
        {
            var expenseDate = new DateTime(2019, 12, 1);

            this._user.AddFixedExpense("Test fixed expense", Category.Taxes, expenseDate, new TransactionValue(300),
                Recurrency.Semiannualy, DateTime.Now.AddMonths(9));

            Assert.AreEqual(2, this._user.Expenses.Count());

            foreach (var expense in this._user.Expenses)
            {
                Assert.AreEqual(1, expense.Date.Day);
            }
        }

        [TestMethod]
        public void WhenUserRegisterFixedExpenseWithAnnualRecurrencyTheExpenseWillAppearUntilTheLimitDate()
        {
            var expenseDate = new DateTime(2019, 12, 1);

            this._user.AddFixedExpense("Test fixed expense", Category.Taxes, expenseDate, new TransactionValue(300),
                Recurrency.Annual, DateTime.Now.AddMonths(12));

            Assert.AreEqual(2, this._user.Expenses.Count());

            foreach (var expense in this._user.Expenses)
            {
                Assert.AreEqual(1, expense.Date.Day);
                Assert.AreEqual(12, expense.Date.Month);
            }
        }


        [TestMethod]
        [ExpectedException(typeof(ExpenseMustHaveName))]
        public void UserCannotAddNamelessExpense()
        {
            this._user.AddExpense(null, Category.Others, DateTime.MinValue, TransactionValue.Zero);
        }

        [TestMethod]
        [ExpectedException(typeof(ExpenseTransactionValueCannotBeNull))]
        public void UserCannotAddNullExpenceValue()
        {
            this._user.AddExpense("Test expense", Category.Others, new DateTime(2018, 10, 23), null);
        }

        [TestMethod]
        [ExpectedException(typeof(TransactionValueCannotBeNegative))]
        public void UserCannotAddNegativeExpenseValue()
        {
            this._user.AddExpense("Test Expense", Category.Others, new DateTime(2018, 10, 23), new TransactionValue(-200));
        }

        [TestMethod]
        public void UserExepenseHasCategory()
        {
            this._user.AddExpense("Test Expense", Category.Others, DateTime.Now, new TransactionValue(200));
            Assert.IsNotNull(this._user.Expenses.First().Category);
        }

        [TestMethod]
        [ExpectedException(typeof(ExpenseCannotBeOlderThanOneHundredYears))]
        public void UserCannotAddExpenseOlderThanTenYears()
        {
            this._user.AddExpense("Test Expense", Category.Others, DateTime.Now.AddYears(-100), new TransactionValue(200));
        }

        [TestMethod]
        [ExpectedException(typeof(ExpenseNotFound))]
        public void ThrowsExpenseNotFoundWhenUserUserTrysToAlterAnExpenseThatDoesntExist()
        {
            this._user.AddExpense("Edited", Category.Others, new DateTime(2019, 10, 20), new TransactionValue(30));
            this._user.EditExpense(Guid.NewGuid(), "Edited Expense", Category.Others, new DateTime(2018, 10, 20), new TransactionValue(303m));
        }

        [TestMethod]
        public void UserCanAlterExpense()
        {
            var newExpenseNumber = this._user.AddExpense("Edited", Category.Others, new DateTime(2019, 10, 20), new TransactionValue(30));
            this._user.EditExpense(newExpenseNumber, "Edited Expense", Category.Others, new DateTime(2018, 10, 20), new TransactionValue(303m));

            Assert.AreEqual("Edited Expense", this._user.Expenses.First().Name);
            Assert.AreEqual(new DateTime(2018, 10, 20), this._user.Expenses.First().Date);
            Assert.AreEqual(new TransactionValue(303m), this._user.Expenses.First().Value);
        }

        [TestMethod]
        public void TotalSpentOfTheUserIsTheSumOfAllExpenses()
        {
            this._user.AddExpense("Test expense", Category.Others, DateTime.Now, new TransactionValue(342.12m));
            this._user.AddExpense("Test expense", Category.Others, DateTime.Now, new TransactionValue(300.12m));
            this._user.AddExpense("Test expense", Category.Others, DateTime.Now, new TransactionValue(1000.12m));
            this._user.AddExpense("Test expense", Category.Others, DateTime.Now, new TransactionValue(1.12m));

            Assert.AreEqual(1643.48m, this._user.Expenses.Sum(e => e.Value.Value));
        }

        [TestMethod]
        public void UserCanRemoveExpense()
        {
            var expenseNumber1 = this._user.AddExpense("Test expense", Category.Others, DateTime.Now, new TransactionValue(200.90m));
            var expenseNumber2 = this._user.AddExpense("Test expense", Category.Others, DateTime.Now, new TransactionValue(200.90m));
            this._user.RemoveExpense(expenseNumber1);

            Assert.AreEqual(1, this._user.Expenses.Count());
            Assert.AreEqual(expenseNumber2, this._user.Expenses.First().Number);
        }

        [TestMethod]
        [ExpectedException(typeof(ExpenseNotFound))]
        public void ThrowsExpenseNotFoundWhenUserTriesToRemoveAnInexistentExpense()
        {
            this._user.AddExpense("Test expense", Category.Others, DateTime.Now, new TransactionValue(200.90m));
            this._user.AddExpense("Test expense", Category.Others, DateTime.Now, new TransactionValue(200.90m));
            
            this._user.RemoveExpense(Guid.Empty);
        }
    }
}