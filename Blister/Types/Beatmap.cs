using System;
using Newtonsoft.Json;

namespace Blister.Types
{
    /// <summary>
    /// Playlist Beatmap
    /// </summary>
    public struct Beatmap
    {
        /// <summary>
        /// Beatmap type
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Date this entry was added to the playlist
        /// </summary>
        [JsonProperty("dateAdded")]
        public DateTime DateAdded { get; set; }

        /// <summary>
        /// BeatSaver Key
        /// </summary>
        [JsonProperty("key", NullValueHandling = NullValueHandling.Ignore)]
        public string Key { get; set; }

        /// <summary>
        /// Beatmap sha1sum
        /// </summary>
        [JsonProperty("hash", NullValueHandling = NullValueHandling.Ignore)]
        public string Hash { get; set; }

        /// <summary>
        /// Beatmap zip as bytes
        /// </summary>
        [JsonProperty("bytes", NullValueHandling = NullValueHandling.Ignore)]
        public byte[] Bytes { get; set; }

        /// <summary>
        /// Beatmap level ID
        /// </summary>
        [JsonProperty("levelID", NullValueHandling = NullValueHandling.Ignore)]
        public string LevelID { get; set; }
    }
}
