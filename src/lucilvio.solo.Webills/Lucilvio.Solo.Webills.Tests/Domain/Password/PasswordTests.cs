using Lucilvio.Solo.Webills.Profile.Domain.User.BusinessErrors;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lucilvio.Solo.Webills.Tests.Domain.Password
{
    [TestClass]
    public class PasswordTests
    {
        [TestMethod]
        [ExpectedException(typeof(PasswordCannotBeNullOrEmpty))]
        public void ThrowsPasswordCannotBeNullOrEmptyWhenPasswordIsEmpty()
        {
            new Profile.Domain.User.Password("");
        }

        [TestMethod]
        [ExpectedException(typeof(PasswordCannotBeNullOrEmpty))]
        public void ThrowsPasswordCannotBeNullOrEmptyWhenLoginIsNull()
        {
            new Profile.Domain.User.Password(null);
        }

        [TestMethod]
        [ExpectedException(typeof(PasswordMustBeGreaterThan6Characters))]
        public void ThrowsPasswordMustBeGreaterThan6CharactersWhenPasswordIsLessThan6Characters()
        {
            new Profile.Domain.User.Password("nfuds");
        }

        [TestMethod]
        public void PasswordIsEqualWhenValueIsTheSame()
        {
            Assert.IsTrue(new Profile.Domain.User.Password("123456")
                .Equals(new Profile.Domain.User.Password("123456")));

            Assert.IsTrue(new Profile.Domain.User.Password("123456") 
                == new Profile.Domain.User.Password("123456"));
        }

        [TestMethod]
        public void LoginIsDifferentWhenValuesAreNotTheSame()
        {
            Assert.IsTrue(!new Profile.Domain.User.Password("123456")
                .Equals(new Profile.Domain.User.Password("654321")));

            Assert.IsTrue(new Profile.Domain.User.Password("123456") 
                != new Profile.Domain.User.Password("654321"));
        }

        [TestMethod]
        public void ReturnValidPassword()
        {
            var password = new Profile.Domain.User.Password("123456");

            Assert.AreEqual("123456", password.Value);
        }
    }
}
