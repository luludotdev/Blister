using System;

namespace Blister.Conversion.Types
{
    /// <summary>
    /// Flags used to control conversion logic
    /// </summary>
    [Flags]
    public enum ConversionFlags : byte
    {
        /// <summary>
        /// Conversion will fail on any invalid entries or metadata
        /// </summary>
        Strict = 0,

        /// <summary>
        /// Entries with invalid hashes will be skipped instead of throwing
        /// </summary>
        IgnoreInvalidHashes = 1 << 0,

        /// <summary>
        /// Entries with invalid keys will be skipped instead of throwing
        /// </summary>
        IgnoreInvalidKeys = 1 << 1,

        /// <summary>
        /// Legacy playlist with an invalid cover image will have the resulting cover set to <code>null</code> instead of throwing
        /// </summary>
        IgnoreInvalidCover = 1 << 2,

        /// <summary>
        /// Conversion will skip or gracefully handle invalid entries or metadata
        /// </summary>
        Loose = IgnoreInvalidHashes | IgnoreInvalidKeys | IgnoreInvalidCover,

        /// <summary>
        /// Sensible defaults for conversion
        /// </summary>
        Default = IgnoreInvalidCover,
    }

    internal static class FlagUtils
    {
        internal static bool HasFlag(ConversionFlags a, ConversionFlags b)
        {
            return (a & b) == b;
        }
    }
}
