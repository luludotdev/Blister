using System;

namespace Blister.Types
{
    /// <summary>
    /// Playlist Beatmap Type
    /// </summary>
    public enum BeatmapType : uint
    {
        /// <summary>
        /// BeatSaver Key
        /// </summary>
        Key = 0,

        /// <summary>
        /// Beatmap Hash
        /// </summary>
        Hash = 1,

        /// <summary>
        /// Compressed Beatmap Zip
        /// </summary>
        Zip = 2,

        /// <summary>
        /// Beatmap Level ID
        /// </summary>
        LevelID = 3,
    }
}
