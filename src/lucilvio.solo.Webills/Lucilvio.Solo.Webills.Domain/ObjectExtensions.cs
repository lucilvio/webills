namespace Lucilvio.Solo.Webills.Domain
{
    public static class ObjectExtensions
    {
        public static bool NotFound(this object obj)
        {
            return obj == null;
        }
    }
}
