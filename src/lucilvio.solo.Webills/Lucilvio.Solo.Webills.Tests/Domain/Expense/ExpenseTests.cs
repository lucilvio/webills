using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.Tests.Domain.Expense
{
    [TestClass]
    public class ExpenseTests
    {
        [TestMethod]
        public void ExpenseHasCategory()
        {
            var user = new Webills.Domain.User.User("Test user");
            user.AddExpense("Test expense", DateTime.Now, new Webills.Domain.User.TransactionValue(200));
        }
    }
}
