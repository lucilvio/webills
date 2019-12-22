using Microsoft.VisualStudio.TestTools.UnitTesting;

using Lucilvio.Solo.Webills.Domain.User;
using Lucilvio.Solo.Webills.Domain.User.BusinessErrors;

namespace Lucilvio.Solo.Webills.Tests.Domain.Login
{
    [TestClass]
    public class LoginTests
    {
        [TestMethod]
        [ExpectedException(typeof(LoginCannotBeNullOrEmpty))]
        public void ThrowLoginCannotBeNullOrEmptyWhenLoginIsEmpty()
        {
            new Webills.Domain.User.Login("");
        }

        [TestMethod]
        [ExpectedException(typeof(LoginCannotBeNullOrEmpty))]
        public void ThrowLoginCannotBeNullOrEmptyWhenLoginIsNull()
        {
            new Webills.Domain.User.Login(null);
        }

        [TestMethod]
        [ExpectedException(typeof(LoginIsNotAValidEmail))]
        public void ThrowLoginMustBeAValidEmail()
        {
            new Webills.Domain.User.Login("sample.com");
        }

        [TestMethod]
        public void LoginIsEqualWhenValueIsTheSame()
        {
            Assert.IsTrue(new Webills.Domain.User.Login("sample@mail.com") == new Webills.Domain.User.Login("sample@mail.com"));
            Assert.IsTrue(new Webills.Domain.User.Login("sample@mail.com").Equals(new Webills.Domain.User.Login("sample@mail.com")));
        }

        [TestMethod]
        public void LoginIsDifferentWhenValuesAreNotTheSame()
        {
            Assert.IsTrue(new Webills.Domain.User.Login("sample@mail.com") != new Webills.Domain.User.Login("sampl@mail.com"));
            Assert.IsTrue(!new Webills.Domain.User.Login("sample@mail.com").Equals(new Webills.Domain.User.Login("sampl@mail.com")));
        }

        [TestMethod]
        public void ReturnValidLogin()
        {
            var login = new Webills.Domain.User.Login("sample@mail.com");

            Assert.AreEqual("sample@mail.com", login.Value);
        } 
    }
}
