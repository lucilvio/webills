
using System;

using NUnit.Framework;

namespace Lucilvio.Solo.Webills.UserAccount.Domain
{
    [TestFixture]
    public class PasswordTests
    {
        [Test]
        public void CantCreateEmptyPassword()
        {
            Assert.Throws<Password.Error.PasswordCantBeNullOrEmpty>(() =>
            {
                new Password(null);
            });
        }

        [Test]
        public void CreatePassword()
        {
            var password = new Password("123456");

            Assert.IsNotNull(password);
            Assert.AreEqual("123456", password.Value);
        }

        [Test]
        public void PasswordsAreEqualWhenValuesAreEqual()
        {
            Assert.True(new Password("123456") == new Password("123456"));
            Assert.True(new Password("123456").Equals(new Password("123456")));
        }

        [Test]
        public void PasswordsAreDifferentWhenValuesAreDifferent()
        {
            Assert.True(new Password("123456") != new Password("654321"));
        }

        [Test]
        public void CantCreateEmptyComplexPassword()
        {
            Assert.Throws<ComplexPassword.Error.PasswordCantBeEmpty>(() =>
            {
                new ComplexPassword(null);
            });
        }

        [Test]
        public void CantCreateComplexPasswordsShorterThanSixCharacters()
        {
            Assert.Throws<ComplexPassword.Error.PasswordMustBeGreaterThanSixCharacters>(() =>
            {
                new ComplexPassword(new Password("12345"));
            });
        }

        [Test]
        public void ComplexPasswordsAreEqualWhenValuesAreEqual()
        {
            Assert.True(new ComplexPassword(new Password("123456")) == new ComplexPassword(new Password("123456")));
            Assert.True(new ComplexPassword(new Password("123456")).Equals(new ComplexPassword(new Password("123456"))));
        }

        [Test]
        public void ComplexPasswordsAreDifferentWhenValuesAreDifferent()
        {
            Assert.True(new ComplexPassword(new Password("123456")) != new ComplexPassword(new Password("1234562")));
        }

        [Test]
        public void CantCreateEmptySh1Passwords()
        {
            Assert.Throws<Sha1EncryptedPassword.Error.PasswordCantBeEmpty>(() =>
            {
                new Sha1EncryptedPassword(null);
            });
        }

        [Test]
        public void Sha1EncryptedPasswordsAreEqualWhenValuesAreEqual()
        {
            Assert.True(new Sha1EncryptedPassword(new Password("123456")) == new Sha1EncryptedPassword(new Password("123456")));
            Assert.True(new Sha1EncryptedPassword(new Password("123456")).Equals(new Sha1EncryptedPassword(new Password("123456"))));
        }

        [Test]
        public void Sha1EncryptedPasswordAreDifferentWhenValuesAreDifferent()
        {
            Assert.True(new Sha1EncryptedPassword(new Password("123456")) != new Sha1EncryptedPassword(new Password("1234562")));
        }

        [Test]
        public void PasswordIsEqualToSh1EncryptedPassword()
        {
            Assert.True(new Password("7C4A8D09CA3762AF61E59520943DC26494F8941B") == new Sha1EncryptedPassword(new Password("123456")));
        }
    }
}
