namespace Lucilvio.Solo.Architecture
{
    public interface IHandlerFactory<TContainer>
    {
        void Create(TContainer container, object parameters);
    }
}