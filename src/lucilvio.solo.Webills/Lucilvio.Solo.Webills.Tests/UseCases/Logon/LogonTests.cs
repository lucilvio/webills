using Moq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Lucilvio.Solo.Webills.Domain.User;
using Lucilvio.Solo.Webills.UseCases.Common;
using Lucilvio.Solo.Webills.UseCases.Contracts.Logon;
using Lucilvio.Solo.Webills.UseCases.Logon;
using Lucilvio.Solo.Webills.Domain.User.BusinessErrors;

namespace Lucilvio.Solo.Webills.Tests.UseCases.Logon
{
    [TestClass]
    public class LogonTests
    {
        [TestMethod]
        [ExpectedException(typeof(DataStorageNotInformed))]
        public void ThrowsDataStorageNotInformedWhenDataStorageIsNull()
        {
            new Webills.UseCases.Logon.Logon(null);
        }

        [TestMethod]
        [ExpectedException(typeof(CommandNotInformed))]
        public async Task ThrowsCommandNotInformedWhenCommandIsNull()
        {
            await new Webills.UseCases.Logon.Logon(new Mock<ILogonDataStorage>().Object).Execute(null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserOrPassword))]
        public async Task ThrowsInvalidUserOrPasswordIfNoUserCanBeFoundWithLoginAndPasswordInformed()
        {
            var dataStorageWithoutUsersMock = new Mock<ILogonDataStorage>();
            dataStorageWithoutUsersMock.Setup(mock => mock.GetUserByLogin(It.IsAny<Login>()))
                .ReturnsAsync(null as User);

            await new Webills.UseCases.Logon.Logon(dataStorageWithoutUsersMock.Object).Execute(
                new CommandMock(new Login("sample@mail.com"), new Password("123456")));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserOrPassword))]
        public async Task ThrowsInvalidUserOrPasswordIfFoundUserHasDifferentPassword()
        {
            var userLogin = "user@mail.com";

            var dataStorageWithUsersMock = new Mock<ILogonDataStorage>();
            dataStorageWithUsersMock.Setup(mock => mock.GetUserByLogin(new Login(userLogin)))
                .ReturnsAsync(new User("Sample User", new Login(userLogin), new Password("654321")));

            await new Webills.UseCases.Logon.Logon(dataStorageWithUsersMock.Object).Execute(
                new CommandMock(new Login(userLogin), new Password(userLogin)));
        }

        [TestMethod]
        public async Task LoginSucessfully()
        {
            var userLogin = "user@mail.com";
            var userPassword = "123456";

            var dataStorageWithUsersMock = new Mock<ILogonDataStorage>();
            dataStorageWithUsersMock.Setup(mock => mock.GetUserByLogin(new Login(userLogin)))
                .ReturnsAsync(new User("Sample User", new Login(userLogin), new Password(userPassword)));

            var useCase = new Webills.UseCases.Logon.Logon(dataStorageWithUsersMock.Object);
            
            await useCase.Execute(new CommandMock(new Login(userLogin), new Password(userPassword)));
        }
    }

    public class CommandMock : LogonCommand
    {
        public CommandMock(Login login, Password password)
        {
            base.Login = login;
            base.Password = password;
        }
    }
}
