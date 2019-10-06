using Lucilvio.Solo.Webills.Domain.User;
using Lucilvio.Solo.Webills.UseCases.AddNewIncome;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Lucilvio.Solo.Webills.Tests.UseCases.AddNewExpense
{
    [TestClass]
    public class AddNewExpenseTests
    {
        [TestMethod]
        [ExpectedException(typeof(CommandNotInformed))]
        public void ThrowsCommandNotInformedWhenCommandIsNull()
        {
            var addNewExpense = new Webills.UseCases.AddNewExpense.AddNewExpense(new AddNewExpenseDataStorageWithTestUserStub());
            addNewExpense.Execute(null);
        }

        [TestMethod]
        [ExpectedException(typeof(UserNotFound))]
        public void ThrowsUerNotFoundExceptionWhenTheUserIsNotFound()
        {
            var addNewExpense = new Webills.UseCases.AddNewExpense.AddNewExpense(new AddNewExpenseDataStorageWithNullUserStub());
            addNewExpense.Execute(new AddNewExpenseCommandStub("Test income", DateTime.Now, TransactionValue.Zero));
        }
    }
}
