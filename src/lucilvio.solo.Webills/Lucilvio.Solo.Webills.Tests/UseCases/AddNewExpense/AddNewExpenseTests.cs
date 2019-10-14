using Lucilvio.Solo.Webills.Domain.User;
using Lucilvio.Solo.Webills.UseCases.AddNewIncome;
using Lucilvio.Solo.Webills.UseCases.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.Tests.UseCases.AddNewExpense
{
    [TestClass]
    public class AddNewExpenseTests
    {
        [TestMethod]
        [ExpectedException(typeof(CommandNotInformed))]
        public async Task ThrowsCommandNotInformedWhenCommandIsNull()
        {
            var addNewExpense = new Webills.UseCases.AddNewExpense.AddNewExpense(new AddNewExpenseDataStorageWithTestUserStub());
            await addNewExpense.Execute(null);
        }

        [TestMethod]
        [ExpectedException(typeof(UserNotFound))]
        public async Task ThrowsUerNotFoundExceptionWhenTheUserIsNotFound()
        {
            var addNewExpense = new Webills.UseCases.AddNewExpense.AddNewExpense(new AddNewExpenseDataStorageWithNullUserStub());
            await addNewExpense.Execute(new AddNewExpenseCommandStub("Test income", DateTime.Now, TransactionValue.Zero));
        }
    }
}
