using Autofac;
using Lucilvio.Solo.Architecture;
using Lucilvio.Solo.Webills.UserAccount.Infraestructure.DataAccess;
using Lucilvio.Solo.Webills.UserAccount.Login;

namespace Lucilvio.Solo.Webills.UserAccount.Infraestructure.Injection
{
    internal class LoginFactory : IHandlerFactory<ContainerBuilder>
    {
        public void Create(ContainerBuilder container, object parameters)
        {
            container.Register(c => new UserAccountDataContext((Configurations)parameters)).AsSelf()
                .InstancePerLifetimeScope();

            container.RegisterType<LoginDataAccess>().AsSelf().InstancePerLifetimeScope();
            container.RegisterType<Login.Login>().As<IHandler<LoginMessage>>().InstancePerLifetimeScope();
        }
    }
}