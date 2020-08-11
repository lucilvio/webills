using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Lucilvio.Solo.Webills.Clients.Web.Shared
{
    internal class ExceptionTranslation
    {
        internal static string Translate(Exception exception)
        {
            var typeNameWithSpaces = Regex.Replace(exception.GetType().Name, "([A-Z])", " $1", RegexOptions.Compiled).Trim();

            return CaptalizeFirstLetter(typeNameWithSpaces.ToLower());
        }

        internal static string CaptalizeFirstLetter(string data)
        {
            var chars = data.ToCharArray();

            var charac = data.First(char.IsLetter);
            var i = data.IndexOf(charac);

            chars[i] = char.ToUpper(chars[i]);

            return new string(chars);
        }
    }
}