using System;

using Lucilvio.Solo.Webills.UserAccount.Domain;

using Xunit;

namespace Lucilvio.Solo.Webills.UserAccount.Tests
{
    public class UserTests
    {
        [Fact]
        public void ThrowsNameIsRequiredIfNameIsNotInformed()
        {
            Assert.Throws<User.Error.CannotCreateUserWithoutName>(() =>
            {
                var newUser = new User(new Name(""), new Domain.Login("test@test.com"), new Password("123456"), true); ;
            });
        }

        [Fact]
        public void HasId()
        {
            var newUser = new User(new Name("test user"), new Domain.Login("test@test.com"), new Password("123456"), true);

            Assert.NotEqual(Guid.Empty, newUser.Id);
        }
    }
}
