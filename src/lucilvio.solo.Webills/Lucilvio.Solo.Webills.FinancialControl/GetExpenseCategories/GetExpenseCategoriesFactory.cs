using Autofac;
using Lucilvio.Solo.Architecture;

namespace Lucilvio.Solo.Webills.FinancialControl.GetExpenseCategories
{
    internal class GetExpenseCategoriesFactory : IHandlerFactory<ContainerBuilder>
    {
        public void Create(ContainerBuilder container, object parameters)
        {
            container.RegisterType<GetExpenseCategories>().As<IMessageHandler<GetExpenseCategoriesMessage>>().SingleInstance();
        }
    }
}