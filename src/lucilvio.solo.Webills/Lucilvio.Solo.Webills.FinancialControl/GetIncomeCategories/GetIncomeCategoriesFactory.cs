using Autofac;
using Lucilvio.Solo.Architecture;

namespace Lucilvio.Solo.Webills.FinancialControl.GetIncomeCategories
{
    internal class GetIncomeCategoriesFactory : IHandlerFactory<ContainerBuilder>
    {
        public void Create(ContainerBuilder container, object parameters)
        {
            container.RegisterType<GetIncomeCategories>().As<IHandler<GetIncomeCategoriesMessage>>().SingleInstance();
        }
    }
}