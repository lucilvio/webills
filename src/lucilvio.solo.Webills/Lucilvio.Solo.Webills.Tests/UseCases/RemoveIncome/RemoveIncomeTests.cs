using System;
using System.Linq;
using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Core.Domain.User.BusinessErrors;
using Lucilvio.Solo.Webills.UseCases.Shared.Errors;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lucilvio.Solo.Webills.Tests.UseCases.RemoveIncome
{
    [TestClass]
    public class RemoveIncomeTests
    {
        [TestMethod]
        [ExpectedException(typeof(CommandNotInformed))]
        public async Task ThrowsCommandNotInformedIfCommandIsNull()
        {
            await new Core.UseCases.RemoveIncome.RemoveIncome(new RemoveIncomeDataStorageStubWithOneUserWithIncome()).Execute(null);
        }

        [TestMethod]
        [ExpectedException(typeof(UserNotFound))]
        public async Task ThrowsUserNotFoundIfAnyUserWasFound()
        {
            await new Core.UseCases.RemoveIncome.RemoveIncome(new RemoveIncomeDataStorageStubWithoutUser()).Execute(new RemoveIncomeCommandStub(Guid.Empty));
        }

        [TestMethod]
        [ExpectedException(typeof(IncomeNotFound))]
        public async Task ThrowsIncomeNotFoundIfAnyIncomeWasFound()
        {
            await new Core.UseCases.RemoveIncome.RemoveIncome(new RemoveIncomeDataStorageStubWithOneUserWithIncome()).Execute(new RemoveIncomeCommandStub(Guid.Empty));
        }

        [TestMethod]
        public async Task UserCanRemoveIncome()
        {
            var dataStorage = new RemoveIncomeDataStorageStubWithOneUserWithIncome();
            var user = await dataStorage.GetUser();
            
            var useCase = new Core.UseCases.RemoveIncome.RemoveIncome(dataStorage);
            await useCase.Execute(new RemoveIncomeCommandStub(user.Incomes.First().Id));

            Assert.AreEqual(0, user.Incomes.Count());
        }
    }
}