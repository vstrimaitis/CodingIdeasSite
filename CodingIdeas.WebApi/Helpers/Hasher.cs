using System.Security.Cryptography;
using System.Text;

namespace CodingIdeas.WebApi.Helpers
{
    internal class Hasher
    {
        public static string ComputeSha256(string text)
        {
            var bytes = Encoding.UTF8.GetBytes(text);
            SHA256Managed hasher = new SHA256Managed();
            var hash = hasher.ComputeHash(bytes);
            var builder = new StringBuilder(string.Empty);
            foreach (var b in hash)
                builder.AppendFormat("{0:x2}", b);
            return builder.ToString();
        }
    }
}