using Lucilvio.Solo.Webills.UserAccount.Domain;

using Xunit;

namespace Lucilvio.Solo.Webills.UserAccount.Tests
{
    public class PasswordTests
    {
        [Fact]
        public void ThrowsPasswordCannotBeNullOrEmptyWhenPasswordIsEmpty()
        {
            Assert.Throws<Password.Error.PasswordCannotBeNullOrEmpty>(() =>
            {
                new Password("");
            });
        }

        [Fact]
        public void ThrowsPasswordCannotBeNullOrEmptyWhenLoginIsNull()
        {
            Assert.Throws<Password.Error.PasswordCannotBeNullOrEmpty>(() =>
            {
                new Password(null);
            });
        }

        [Fact]
        public void PasswordIsEqualWhenValueIsTheSame()
        {
            Assert.True(new Password("123456").Equals(new Password("123456")));
            Assert.True(new Password("123456") == new Password("123456"));
        }

        [Fact]
        public void LoginIsDifferentWhenValuesAreNotTheSame()
        {
            Assert.True(!new Password("123456").Equals(new Password("654321")));

            Assert.True(new Password("123456") != new Password("654321"));
        }

        [Fact]
        public void ReturnValidPassword()
        {
            var password = new Password("123456");

            Assert.Equal("123456", password.Value);
        }
    }
}
