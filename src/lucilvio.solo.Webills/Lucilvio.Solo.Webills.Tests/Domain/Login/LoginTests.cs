using Microsoft.VisualStudio.TestTools.UnitTesting;

using Lucilvio.Solo.Webills.Profile.Domain.User.BusinessErrors;

namespace Lucilvio.Solo.Webills.Tests.Domain.Login
{
    [TestClass]
    public class LoginTests
    {
        [TestMethod]
        [ExpectedException(typeof(LoginCannotBeNullOrEmpty))]
        public void ThrowLoginCannotBeNullOrEmptyWhenLoginIsEmpty()
        {
            new Profile.Domain.User.Login("");
        }

        [TestMethod]
        [ExpectedException(typeof(LoginCannotBeNullOrEmpty))]
        public void ThrowLoginCannotBeNullOrEmptyWhenLoginIsNull()
        {
            new Profile.Domain.User.Login(null);
        }

        [TestMethod]
        [ExpectedException(typeof(LoginIsNotAValidEmail))]
        public void ThrowLoginMustBeAValidEmail()
        {
            new Profile.Domain.User.Login("sample.com");
        }

        [TestMethod]
        public void LoginIsEqualWhenValueIsTheSame()
        {
            Assert.IsTrue(new Profile.Domain.User.Login("sample@mail.com") == new Profile.Domain.User.Login("sample@mail.com"));
            Assert.IsTrue(new Profile.Domain.User.Login("sample@mail.com").Equals(new Profile.Domain.User.Login("sample@mail.com")));
        }

        [TestMethod]
        public void LoginIsDifferentWhenValuesAreNotTheSame()
        {
            Assert.IsTrue(new Profile.Domain.User.Login("sample@mail.com") != new Profile.Domain.User.Login("sampl@mail.com"));
            Assert.IsTrue(!new Profile.Domain.User.Login("sample@mail.com").Equals(new Profile.Domain.User.Login("sampl@mail.com")));
        }

        [TestMethod]
        public void ReturnValidLogin()
        {
            var login = new Profile.Domain.User.Login("sample@mail.com");

            Assert.AreEqual("sample@mail.com", login.Value);
        } 
    }
}
