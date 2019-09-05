using System;

namespace Blister.Conversion
{
    /// <summary>
    /// Raised when trying to convert a base64 image to bytes
    /// </summary>
    public class InvalidBase64Exception : Exception { }

    /// <summary>
    /// Raised when a BeatSaver map key is invalid
    /// </summary>
    public class InvalidMapKeyException : Exception
    {
        /// <summary>
        /// Instantiate a new InvalidMapKeyException
        /// </summary>
        /// <param name="key">Invalid BeatSaver Key</param>
        public InvalidMapKeyException(string key)
        {
            Key = key;
        }

        /// <summary>
        /// Invalid BeatSaver Key
        /// </summary>
        public string Key;
    }

    /// <summary>
    /// Raised when a beatmap's hash is invalid (invalid sha1)
    /// </summary>
    public class InvalidMapHashException : Exception
    {
        /// <summary>
        /// Instantiate a new InvalidMapHashException
        /// </summary>
        /// <param name="hash">Invalid Hash</param>
        public InvalidMapHashException(string hash)
        {
            Hash = hash;
        }

        /// <summary>
        /// Invalid Hash
        /// </summary>
        public string Hash;
    }
}
