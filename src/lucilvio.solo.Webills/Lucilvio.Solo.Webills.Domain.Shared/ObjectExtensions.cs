namespace Lucilvio.Solo.Webills.Domain.Shared
{
    public static class ObjectExtensions
    {
        public static bool NotDefined(this object obj) => obj == null;
        public static bool IsDefined(this object obj) => obj != null;
    }
}
