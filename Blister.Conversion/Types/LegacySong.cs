using System;
using Newtonsoft.Json;

namespace Blister.Conversion.Types
{
    /// <summary>
    /// Legacy playlist beatmap
    /// </summary>
    public class LegacySong
    {
        /// <summary>
        /// BeatSaver Key
        /// <br />
        /// Possibly in the old format
        /// </summary>
        [JsonProperty("key", NullValueHandling = NullValueHandling.Ignore)]
        public string Key { get; set; }

        /// <summary>
        /// Beat Saber beatmapmap hash
        /// <br />
        /// Possibly in the old (md5) format
        /// </summary>
        [JsonProperty("hash", NullValueHandling = NullValueHandling.Ignore)]
        public string Hash { get; set; }

        /// <summary>
        /// Beat Saber Beatmap level ID
        /// </summary>
        [JsonProperty("levelId", NullValueHandling = NullValueHandling.Ignore)]
        public string LevelID { get; set; }
    }
}
