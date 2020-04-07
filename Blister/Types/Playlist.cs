using System;
using System.Collections.Generic;
using Blister.IO;

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
        public string Title;

        /// <summary>
        /// Playlist author
        /// </summary>
        public string Author;

        /// <summary>
        /// Playlist description
        /// <br />
        /// Nullable
        /// </summary>
        public string? Description;

        /// <summary>
        /// Playlist cover image as bytes
        /// </summary>
        public byte[]? Cover;

        /// <summary>
        /// Playlist beatmaps
        /// </summary>
        public List<Beatmap> Maps;

        /// <summary>
        /// Creates a new empty playlist with the specified metadata
        /// </summary>
        /// <param name="title">Playlist title</param>
        /// <param name="author">Playlist author</param>
        /// <param name="description">Optional playlist description</param>
        /// <param name="cover">Optional playlist cover</param>
        public Playlist(string title, string author, string? description = null, byte[]? cover = null)
        {
            Title = title;
            Author = author;
            Description = description;
            Cover = cover;
            Maps = new List<Beatmap>();
        }

        /// <summary>
        /// Reads a playlist from a stream
        /// </summary>
        /// <param name="reader">Binary Reader</param>
        internal Playlist(BlisterBinaryReader reader)
        {
            Title = reader.ReadShortString();
            Author = reader.ReadShortString();
            Description = reader.ReadOptionalLongString();
            Cover = reader.ReadOptionalBytes();

            var mapCount = (int)reader.ReadUInt32();
            var maps = new List<Beatmap>(mapCount);
            for (var i = 0; i < mapCount; i++)
            {
                maps.Add(new Beatmap(reader));
            }

            Maps = maps;
        }

        /// <summary>
        /// Writes the playlist to a stream
        /// </summary>
        /// <param name="writer">Binary Writer</param>
        internal void Write(BlisterBinaryWriter writer)
        {
            writer.WriteShortString(Title);
            writer.WriteShortString(Author);
            writer.WriteLongString(Description);
            writer.WriteBytes(Cover);

            writer.Write((uint)Maps.Count);
            foreach (var map in Maps)
            {
                map.Write(writer);
            }
        }
    }
}
