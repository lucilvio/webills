using Lucilvio.Solo.Webills.Core.Domain.User;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lucilvio.Solo.Webills.Tests
{
    public class UserTests
    {
        [TestMethod]
        public void UserHasName()
        {
            var user = new User("Test User");
            Assert.IsTrue(!string.IsNullOrEmpty(user.Name));
        }
    }
}
