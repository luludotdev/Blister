using System;
using Newtonsoft.Json;

namespace Blister.Types
{
    public struct Beatmap
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("dateAdded")]
        public DateTime DateAdded { get; set; }

        [JsonProperty("key", NullValueHandling = NullValueHandling.Ignore)]
        public string Key { get; set; }

        [JsonProperty("hash", NullValueHandling = NullValueHandling.Ignore)]
        public string Hash { get; set; }

        [JsonProperty("bytes", NullValueHandling = NullValueHandling.Ignore)]
        public byte[] Bytes { get; set; }

        [JsonProperty("levelID", NullValueHandling = NullValueHandling.Ignore)]
        public string LevelID { get; set; }
    }
}
