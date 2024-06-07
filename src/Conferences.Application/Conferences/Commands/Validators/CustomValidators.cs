using System.Text.RegularExpressions;

namespace Conferences.Application.Conferences.Commands.Validators
{
    public class CustomValidators
    {
        public static bool BeAValidUrl(string? url)
        {
            if (string.IsNullOrEmpty(url)) return true;

            string urlPattern = @"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$";
            Regex Rgx = new Regex(urlPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return Rgx.IsMatch(url);
        }
    }
}
