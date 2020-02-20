using Xunit;
using Lucilvio.Solo.Webills.UserProfile.Domain;
using Lucilvio.Solo.Webills.UserProfile.Domain.BusincessErrors;

namespace Lucilvio.Solo.Webills.UserProfile.Tests
{
    public class PasswordTests
    {
        [Fact]
        public void ThrowsPasswordCannotBeNullOrEmptyWhenPasswordIsEmpty()
        {
            Assert.Throws<PasswordCannotBeNullOrEmpty>(() =>
            {
                new Password("");
            });
        }

        [Fact]
        public void ThrowsPasswordCannotBeNullOrEmptyWhenLoginIsNull()
        {
            Assert.Throws<PasswordCannotBeNullOrEmpty>(() =>
            {
                new Password(null);
            });
        }

        [Fact]
        public void ThrowsPasswordMustBeGreaterThan6CharactersWhenPasswordIsLessThan6Characters()
        {
            Assert.ThrowsAny<PasswordMustBeGreaterThan6Characters>(() =>
            {
                new Password("nfuds");
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
