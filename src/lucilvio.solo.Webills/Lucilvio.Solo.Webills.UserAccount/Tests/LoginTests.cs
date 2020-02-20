using Lucilvio.Solo.Webills.UserAccount.Domain;

using Xunit;

namespace Lucilvio.Solo.Webills.UserProfile.Tests
{
    public class LoginTests
    {
        [Fact]
        public void ThrowLoginCannotBeNullOrEmptyWhenLoginIsEmpty()
        {
            Assert.Throws<Login.Error.LoginCannotBeEmpty>(() =>
            {
                new Login("");
            });
        }

        [Fact]
        public void ThrowLoginMustBeAValidEmail()
        {
            Assert.Throws<Login.Error.LoginIsNotAValidEmail>(() =>
            {
                new Login("sample.com");
            });
        }

        [Fact]
        public void LoginIsEqualWhenValueIsTheSame()
        {
            Assert.True(new Login("sample@mail.com") == new Login("sample@mail.com"));
            Assert.True(new Login("sample@mail.com").Equals(new Login("sample@mail.com")));
        }

        [Fact]
        public void LoginIsDifferentWhenValuesAreNotTheSame()
        {
            Assert.True(new Login("sample@mail.com") != new Login("sampl@mail.com"));
            Assert.True(!new Login("sample@mail.com").Equals(new Login("sampl@mail.com")));
        }

        [Fact]
        public void ReturnValidLogin()
        {
            var login = new Login("sample@mail.com");

            Assert.Equal("sample@mail.com", login.Value);
        }
    }
}
