using System.Collections.Generic;
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
                
                foreach (var parameter in parameters)
                {
                    builder.Append($"{parameter.Key}={UrlEncoder.Default.Encode(parameter.Value)}");
                }

                builder.Remove(builder.Length - 1, 1);
            }

            return builder.ToString();
        }
    }
}