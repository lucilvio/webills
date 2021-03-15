using System;

using NUnit.Framework;

namespace Lucilvio.Solo.Webills.UserAccount.Domain
{
    [TestFixture]
    internal class UserTests
    {
        public User ValidUser => new User(new Name("Test User"), new Email("test@mail.com"));

        public User ValidUserWithAccount()
        {
            var user = new User(new Name("Test User"), new Email("test@mail.com"));
            user.CreateAccount(new Login("test@mail.com"), new Password("123456"), new Password("123456"), true, null);

            return user;
        }

        [Test]
        public void CantCreateUserWithoutName()
        {
            Assert.Throws<User.Error.CantCreateUserWithoutName>(() =>
            {
                new User(null, new Email("test@mail.com"));
            });
        }

        [Test]
        public void CantCreateUserWithoutEmail()
        {
            Assert.Throws<User.Error.CantCreateUserWithoutEmail>(() =>
            {
                new User(new Name("Test"), null);
            });
        }

        [Test]
        public void CreateUser()
        {
            var user = this.ValidUser;

            Assert.AreNotEqual(user.Id, Guid.Empty);
            Assert.AreEqual(new Name("Test User"), user.Name);
            Assert.AreEqual(new Email("test@mail.com"), user.Email);
        }

        public void CantCreateNewUserAccountWithoutLogin()
        {
            var user = this.ValidUser;
        }

        [Test]
        public void CantCreateNewUserAccountIfTheresAnAccountWithTheSameLogin()
        {
            var userWithSameLogin = this.ValidUserWithAccount();

            var user = new User(new Name("Test user"), new Email("test@mail.com"));

            Assert.Throws<User.Error.LoginNotAvailable>(() =>
            {
                user.CreateAccount(new Login("test@mail.com"), new Password("123456"), new Password("123456"), true, userWithSameLogin);
            });
        }

        [Test]
        public void CanCreateNewUserAccountIfUserWithSameLoginDoesntHaveAccountAssociated()
        {
            var userWithSameLogin = this.ValidUser;

            var newUser = new User(new Name("Test user"), new Email("test@mail.com"));

            Assert.Throws<User.Error.UserWithSameLoginMustHaveAnAssociatedAccount>(() =>
            {
                newUser.CreateAccount(new Login("test@mail.com"), new Password("123456"), new Password("123456"), true, userWithSameLogin);
            });
        }

        [Test]
        public void UserCantMakeLoginWithoutAccount()
        {
            var user = this.ValidUser;

            Assert.Throws<User.Error.UserDoesntHaveAnAccountAssociated>(() =>
            {
                user.Login(new Password("123456"));
            });
        }

        [Test]
        public void UserCantMakeLoginWithWrongPassword()
        {
            var user = this.ValidUser;
            user.CreateAccount(new Login("test@mail.com"), new Password("123456"), new Password("123456"), true, null);

            Assert.Throws<Account.Error.LoginOrPasswordInvalid>(() =>
            {
                user.Login(new Password("654321"));
            });
        }

        [Test]
        public void CantChangePasswordIfUserHasNoAccountAssociated()
        {
            var user = this.ValidUser;

            Assert.Throws<User.Error.UserDoesntHaveAnAccountAssociated>(() =>
            {
                user.ChangePassword(null);
            });
        }

        [Test]
        public void CantChangePasswordToAnEmptyOne()
        {
            var user = this.ValidUser;
            user.CreateAccount(new Login("test@mail.com"), new Password("123456"), new Password("123456"), true, null);

            Assert.Throws<Account.Error.CantChangePasswordToAnEmptyOne>(() =>
            {
                user.ChangePassword(null);
            });
        }

        [Test]
        public void ChangeUserPassword()
        {
            var user = this.ValidUserWithAccount();
            user.ChangePassword(new Password("654321"));

            user.Login(new Password("654321"));
        }
    }
}