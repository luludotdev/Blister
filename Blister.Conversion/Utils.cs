using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Blister.Conversion
{
    internal static class Utils
    {
        internal static Regex oldKeyRX = new Regex(@"^\d+-(\d+)$", RegexOptions.Compiled);
        internal static Regex newKeyRX = new Regex(@"^[0-9a-f]+$", RegexOptions.Compiled);

        internal static Regex base64RX = new Regex(@"^(?:data:\w+\/\w+;base64,)?((?:[A-Za-z0-9+/]{4})*(?:[A-Za-z0-9+/]{2}==|[A-Za-z0-9+/]{3}=)?)$", RegexOptions.Compiled);
        internal static Regex sha1RX = new Regex(@"^[0-9a-f]{40}$", RegexOptions.Compiled);

        internal static string ParseKey(string key)
        {
            Match isOld = oldKeyRX.Match(key);
            if (isOld.Success)
            {
                string oldKey = isOld.Groups[1].Value;
                int oldKeyInt = int.Parse(oldKey);
                return oldKeyInt.ToString("x");
            }

            bool isNew = newKeyRX.IsMatch(key);
            if (!isNew) return null;

            return key.ToLower();
        }

        internal static byte[] ParseBase64Image(string text)
        {
            Match match = base64RX.Match(text);
            if (!match.Success)
            {
                throw new InvalidBase64Exception();
            }

            try
            {
                string base64 = match.Groups[1].Value;
                return Convert.FromBase64String(base64);
            }
            catch (FormatException)
            {
                throw new InvalidBase64Exception();
            }
        }

        internal static bool ValidHash(string hash)
        {
            return sha1RX.IsMatch(hash);
        }

        internal static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
    }
}
