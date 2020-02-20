using System;

using Lucilvio.Solo.Webills.UserProfile.Domain;
using Lucilvio.Solo.Webills.UserProfile.Domain.BusincessErrors;

using Xunit;

namespace Lucilvio.Solo.Webills.UserProfile.Tests
{
    public class UserTests
    {
        [Fact]
        public void ThrowsNameIsRequiredIfNameIsNotInformed()
        {
            Assert.Throws<NameIsRequired>(() =>
            {
                new User(null, new Login("test@test.com"), new Password("123456"), true);
            });
        }

        [Fact]
        public void HasId()
        {
            Assert.NotEqual(Guid.Empty, new User("test user", new Login("test@test.com"), new Password("123456"), true).Id);
        }

        [Fact]
        public void ThrowsIsNecessaryToAcceptTheTermsWhenUserDoesntAcceptTheTerms()
        {
            Assert.Throws<IsNecessaryToAcceptTheTerms>(() =>
            {
                new User("test User", new Login("test@test.com"), new Password("123456"), false);
            });
        }
    }
}
