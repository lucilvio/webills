namespace Lucilvio.Solo.Architecture
{
    public interface IHandlerFactory<in TContainer>
    {
        void Create(TContainer container, object configurations);
    }
}