using System;

namespace Blister
{
    /// <summary>
    /// Raised when the file being deserialized does not contain a correct magic number
    /// </summary>
    public class InvalidMagicNumberException : Exception { }

    /// <summary>
    /// The exception thrown when trying to read or write a playlist using a big-endian CPU
    /// </summary>
    public class UnsupportedEndiannessException : Exception { }
}
