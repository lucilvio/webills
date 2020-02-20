using Lucilvio.Solo.Webills.UserProfile.Domain;
using Lucilvio.Solo.Webills.UserProfile.Domain.BusincessErrors;
using Xunit;

namespace Lucilvio.Solo.Webills.UserProfile.Tests
{
    public class LoginTests
    {
        [Fact]
        public void ThrowLoginCannotBeNullOrEmptyWhenLoginIsEmpty()
        {
            Assert.Throws<LoginCannotBeNullOrEmpty>(() =>
            {
                new Login("");
            });
        }

        [Fact]
        public void ThrowLoginCannotBeNullOrEmptyWhenLoginIsNull()
        {
            Assert.Throws<LoginCannotBeNullOrEmpty>(() =>
            {
                new Login(null);
            });
        }

        [Fact]
        public void ThrowLoginMustBeAValidEmail()
        {
            Assert.Throws<LoginIsNotAValidEmail>(() =>
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
