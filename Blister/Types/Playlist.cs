using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Blister.Types
{
    /// <summary>
    /// Playlist data structure
    /// </summary>
    public class Playlist
    {
        /// <summary>
        /// Playlist title
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// Playlist author
        /// </summary>
        [JsonProperty("author")]
        public string Author { get; set; }

        /// <summary>
        /// Playlist description
        /// <br />
        /// Nullable
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Playlist cover image as bytes
        /// </summary>
        [JsonProperty("cover", NullValueHandling = NullValueHandling.Include)]
        public byte[] Cover { get; set; }

        /// <summary>
        /// Playlist beatmaps
        /// </summary>
        [JsonProperty("maps")]
        public List<Beatmap> Maps { get; set; }
    }
}
