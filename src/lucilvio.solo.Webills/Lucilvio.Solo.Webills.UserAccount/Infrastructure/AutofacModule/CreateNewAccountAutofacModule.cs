using Autofac;
using Lucilvio.Solo.Webills.UserAccount.CreateNewAccount;
using Lucilvio.Solo.Webills.UserAccount.Infraestructure.DataAccess;

namespace Lucilvio.Solo.Webills.UserAccount.Infraestructure.Injection
{
    internal class CreateNewAccountAutofacModule : Autofac.Module
    {
        private readonly Module.Configurations _configurations;

        public CreateNewAccountAutofacModule(Module.Configurations configurations)
        {
            this._configurations = configurations;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new UserAccountDataContext(this._configurations)).AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterType<CreateNewAccountDataAccess>().As<ICreateNewAccountDataAccess>().InstancePerLifetimeScope();
            builder.RegisterType<CreateNewAccount.CreateNewAccount>().As<IHandler<CreateNewAccountMessage>>().InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
