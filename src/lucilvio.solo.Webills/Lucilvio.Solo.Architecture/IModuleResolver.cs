using System.Threading.Tasks;

namespace Lucilvio.Solo.Architecture
{
    public interface IModuleResolver<TModule> where TModule : class
    {
        Task Resolve(object objectToResolve);
    }
}