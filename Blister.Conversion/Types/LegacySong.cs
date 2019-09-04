using System;
using Newtonsoft.Json;

namespace Blister.Conversion.Types
{
    public struct LegacySong
    {
        [JsonProperty("key", NullValueHandling = NullValueHandling.Ignore)]
        public string Key { get; set; }

        [JsonProperty("hash", NullValueHandling = NullValueHandling.Ignore)]
        public string Hash { get; set; }

        [JsonProperty("levelId", NullValueHandling = NullValueHandling.Ignore)]
        public string LevelID { get; set; }
    }
}
