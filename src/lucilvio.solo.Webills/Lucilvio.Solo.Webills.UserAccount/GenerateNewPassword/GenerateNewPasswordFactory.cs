using Autofac;
using Lucilvio.Solo.Architecture;
using Lucilvio.Solo.Webills.UserAccount.GenerateNewPassword;
using Lucilvio.Solo.Webills.UserAccount.Infraestructure.DataAccess;

namespace Lucilvio.Solo.Webills.UserAccount.Infraestructure.Injection
{
    internal class GenerateNewPasswordFactory : IHandlerFactory<ContainerBuilder>
    {
        public void Create(ContainerBuilder container, object parameters)
        {
            container.Register(c => new UserAccountDataContext((Configurations)parameters)).AsSelf()
                .InstancePerLifetimeScope();

            container.RegisterDecorator<TransactionScopedHandler<GenerateNewPasswordMessage>, IMessageHandler<GenerateNewPasswordMessage>>();
            container.RegisterType<GenerateNewPasswordDataAccess>().AsSelf().InstancePerLifetimeScope();
            container.RegisterType<GenerateNewPassword.GenerateNewPassword>().As<IMessageHandler<GenerateNewPasswordMessage>>().InstancePerLifetimeScope();
        }
    }
}