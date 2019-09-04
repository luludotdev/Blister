using System;

namespace Blister.Conversion
{
    public class InvalidBase64Exception : Exception { }

    public class InvalidMapKeyException : Exception
    {
        public InvalidMapKeyException(string key)
        {
            Key = key;
        }

        public string Key;
    }

    public class InvalidMapHashException : Exception
    {
        public InvalidMapHashException(string hash)
        {
            Hash = hash;
        }

        public string Hash;
    }
}
