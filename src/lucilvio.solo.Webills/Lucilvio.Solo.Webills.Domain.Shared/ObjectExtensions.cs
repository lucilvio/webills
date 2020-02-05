namespace Lucilvio.Solo.Webills.Shared.Domain
{
    public static class ObjectExtensions
    {
        public static bool NotDefined(this object obj) => obj == null;
        public static bool IsDefined(this object obj) => obj != null;
    }
}
