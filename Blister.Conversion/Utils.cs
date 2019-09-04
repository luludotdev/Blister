using System;
using System.Text.RegularExpressions;

namespace Blister.Conversion
{
    public static class Utils
    {
        internal static Regex oldKeyRX = new Regex(@"^\d+-(\d+)$", RegexOptions.Compiled);
        internal static Regex newKeyRX = new Regex(@"^[0-9a-f]+$", RegexOptions.Compiled);

        internal static Regex base64RX = new Regex(@"^.+base64,(.+)$", RegexOptions.Compiled);
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

        public static byte[] ParseBase64Image(string text)
        {
            Match match = base64RX.Match(text);
            if (!match.Success)
            {
                throw new InvalidBase64Exception();
            }

            string base64 = match.Groups[1].Value;
            return Convert.FromBase64String(base64);
        }

        public static bool ValidHash(string hash)
        {
            return sha1RX.IsMatch(hash);
        }
    }
}
