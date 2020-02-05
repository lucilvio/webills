using Moq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Lucilvio.Solo.Webills.UseCases.Logon;
using Lucilvio.Solo.Webills.Security.Domain.User.BusinessErrors;
using Lucilvio.Solo.Webills.Security.Domain.User;
using Lucilvio.Solo.Webills.Security.UseCases.Contracts.Logon;
using Lucilvio.Solo.Webills.Shared.UseCases.Errors;

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
            dataStorageWithoutUsersMock.Setup(mock => mock.GetUserByLogin(It.IsAny<string>()))
                .ReturnsAsync(null as User);

            await new Webills.UseCases.Logon.Logon(dataStorageWithoutUsersMock.Object).Execute(
                new CommandMock("sample@mail.com", "123456"));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserOrPassword))]
        public async Task ThrowsInvalidUserOrPasswordIfFoundUserHasDifferentPassword()
        {
            var userLogin = "user@mail.com";

            var dataStorageWithUsersMock = new Mock<ILogonDataStorage>();
            dataStorageWithUsersMock.Setup(mock => mock.GetUserByLogin(userLogin))
                .ReturnsAsync(new User(userLogin, "654321"));

            await new Webills.UseCases.Logon.Logon(dataStorageWithUsersMock.Object).Execute(
                new CommandMock(userLogin, userLogin));
        }

        [TestMethod]
        public async Task LoginSucessfully()
        {
            var userLogin = "user@mail.com";
            var userPassword = "123456";

            var dataStorageWithUsersMock = new Mock<ILogonDataStorage>();
            dataStorageWithUsersMock.Setup(mock => mock.GetUserByLogin(userLogin))
                .ReturnsAsync(new User(userLogin, userPassword));

            var useCase = new Webills.UseCases.Logon.Logon(dataStorageWithUsersMock.Object);
            
            await useCase.Execute(new CommandMock(userLogin, userPassword));
        }
    }

    public class CommandMock : LogonCommand
    {
        public CommandMock(string login, string password)
        {
            base.Login = login;
            base.Password = password;
        }
    }
}
