using System;
using Newtonsoft.Json;

namespace Blister.Conversion.Types
{
    public struct LegacyPlaylist
    {
        [JsonProperty("playlistTitle")]
        public string PlaylistTitle { get; set; }

        [JsonProperty("playlistAuthor")]
        public string PlaylistAuthor { get; set; }

        [JsonProperty("playlistDescription")]
        public string PlaylistDescription { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("songs")]
        public LegacySong[] Songs { get; set; }
    }
}
