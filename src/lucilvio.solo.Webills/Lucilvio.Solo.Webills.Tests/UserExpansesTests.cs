using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lucilvio.Solo.Webills.Tests
{
    [TestClass]
    public class UserExpansesTests
    {
        [TestMethod]
        public void UserHasExpanses()
        {
            var user = new User();
            Assert.IsNotNull(user.Expanses);
        }

        [TestMethod]
        public void UserCanRegisterExpanse()
        {
            var user = new User();
            user.AddExpanse(new Expanse("Test Expanse", new DateTime(2018, 10, 23), new TransactionValue(300)));

            Assert.IsNotNull(user.Expanses);
            Assert.IsTrue(user.HasExpanses);
        }

        [TestMethod]
        [ExpectedException(typeof(UserCannotAddNullExpanse))]
        public void UserCantRegisterNullExpanse()
        {
            var user = new User();
            user.AddExpanse(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ExpanseTransactionValueCannotBeNull))]
        public void UserCannorAddNullExpanceValue()
        {
            var user = new User();
            user.AddExpanse(new Expanse("Test expanse", new DateTime(2018, 10, 23), null));
        }

        [TestMethod]
        [ExpectedException(typeof(TransactionValueCannotBeNegative))]
        public void UserCannotAddNegativeExpanseValue()
        {
            var user = new User();
            user.AddExpanse(new Expanse("Test Expanse", new DateTime(2018, 10, 23), new TransactionValue(-200)));
        }
    }
}