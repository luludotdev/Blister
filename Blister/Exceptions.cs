using System;

namespace Blister
{
    /// <summary>
    /// Raised when the file being deserialized does not contain a correct magic number
    /// </summary>
    public class InvalidMagicNumberException : Exception { }
}
