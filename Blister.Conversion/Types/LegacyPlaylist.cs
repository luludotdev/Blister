using System;
using Newtonsoft.Json;

namespace Blister.Conversion.Types
{
    /// <summary>
    /// Legacy (v1) Playlist struct
    /// </summary>
    public struct LegacyPlaylist
    {
        /// <summary>
        /// Legacy playlist title
        /// </summary>
        [JsonProperty("playlistTitle")]
        public string PlaylistTitle { get; set; }

        /// <summary>
        /// Legacy playlist author
        /// </summary>
        [JsonProperty("playlistAuthor")]
        public string PlaylistAuthor { get; set; }

        /// <summary>
        /// Legacy playlist description
        /// <br />
        /// Nullable
        /// </summary>
        [JsonProperty("playlistDescription")]
        public string PlaylistDescription { get; set; }

        /// <summary>
        /// Legacy playlist image
        /// <br />
        /// Encoded as a base64 image string
        /// </summary>
        [JsonProperty("image")]
        public string Image { get; set; }

        /// <summary>
        /// Legacy playlist beatmaps
        /// </summary>
        [JsonProperty("songs")]
        public LegacySong[] Songs { get; set; }
    }
}
