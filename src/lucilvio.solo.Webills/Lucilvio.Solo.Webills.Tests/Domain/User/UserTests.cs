
using Lucilvio.Solo.Webills.Domain.User;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lucilvio.Solo.Webills.Tests
{
    public class UserTests
    {
        [TestMethod]
        public void UserHasName()
        {
            var user = new User("Test User", new Login("user@mail.com"), new Password("1234456"));
            Assert.IsTrue(!string.IsNullOrEmpty(user.Name));
        }
    }
}
