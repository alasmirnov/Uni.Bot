using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using Service.Model;

namespace Service.Actions
{
    public static class UrlBuilder
    {
        public static string BuildUrl(string method, string token)
        {
            return BuildUrl(method, token, new Dictionary<string, string>());
        }

        public static string BuildUrl(string method, string token, Dictionary<string, string> parameters)
        {
            var builder = new StringBuilder($"https://api.telegram.org/bot{token}/{method}");

            if (parameters.IsNotEmpty())
            {
                builder.Append('?');

                string parString = parameters.Select(x => $"{x.Key}={UrlEncoder.Default.Encode(x.Value)}")
                    .Aggregate((all, x) => $"{all}&{x}");

                builder.Append(parString);
            }

            return builder.ToString();
        }
    }
}