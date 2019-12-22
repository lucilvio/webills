using Microsoft.VisualStudio.TestTools.UnitTesting;

using Lucilvio.Solo.Webills.Domain.User.BusinessErrors;

namespace Lucilvio.Solo.Webills.Tests.Domain.Password
{
    [TestClass]
    public class PasswordTests
    {
        [TestMethod]
        [ExpectedException(typeof(PasswordCannotBeNullOrEmpty))]
        public void ThrowsPasswordCannotBeNullOrEmptyWhenPasswordIsEmpty()
        {
            new Webills.Domain.User.Password("");
        }

        [TestMethod]
        [ExpectedException(typeof(PasswordCannotBeNullOrEmpty))]
        public void ThrowsPasswordCannotBeNullOrEmptyWhenLoginIsNull()
        {
            new Webills.Domain.User.Password(null);
        }

        [TestMethod]
        [ExpectedException(typeof(PasswordMustBeGreaterThan6Characters))]
        public void ThrowsPasswordMustBeGreaterThan6CharactersWhenPasswordIsLessThan6Characters()
        {
            new Webills.Domain.User.Password("nfuds");
        }

        [TestMethod]
        public void PasswordIsEqualWhenValueIsTheSame()
        {
            Assert.IsTrue(new Webills.Domain.User.Password("123456").Equals(new Webills.Domain.User.Password("123456")));
            Assert.IsTrue(new Webills.Domain.User.Password("123456") == new Webills.Domain.User.Password("123456"));
        }

        [TestMethod]
        public void LoginIsDifferentWhenValuesAreNotTheSame()
        {
            Assert.IsTrue(!new Webills.Domain.User.Password("123456").Equals(new Webills.Domain.User.Password("654321")));
            Assert.IsTrue(new Webills.Domain.User.Password("123456") != new Webills.Domain.User.Password("654321"));
        }

        [TestMethod]
        public void ReturnValidPassword()
        {
            var password = new Webills.Domain.User.Password("123456");

            Assert.AreEqual("123456", password.Value);
        }
    }
}
