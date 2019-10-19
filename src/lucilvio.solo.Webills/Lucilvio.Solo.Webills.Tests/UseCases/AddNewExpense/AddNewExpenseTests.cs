using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.Domain.User;
using Lucilvio.Solo.Webills.UseCases.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lucilvio.Solo.Webills.Tests.UseCases.AddNewExpense
{
    [TestClass]
    public class AddNewExpenseTests
    {
        [TestMethod]
        [ExpectedException(typeof(CommandNotInformed))]
        public async Task ThrowsCommandNotInformedWhenCommandIsNull()
        {
            var addNewExpense = new Webills.UseCases.AddNewExpense.AddNewExpense(new AddNewExpenseDataStorageStubWithTestUser());
            await addNewExpense.Execute(null);
        }

        [TestMethod]
        [ExpectedException(typeof(UserNotFound))]
        public async Task ThrowsUerNotFoundExceptionWhenTheUserIsNotFound()
        {
            var addNewExpense = new Webills.UseCases.AddNewExpense.AddNewExpense(new AddNewExpenseDataStorageStubWithoutUser());
            await addNewExpense.Execute(new AddNewExpenseCommandStub("Test income", DateTime.Now, TransactionValue.Zero));
        }
    }
}
