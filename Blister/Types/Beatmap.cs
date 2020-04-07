using System;
using Blister.IO;

namespace Blister.Types
{
    /// <summary>
    /// Playlist Beatmap
    /// </summary>
    public class Beatmap
    {
        /// <summary>
        /// Beatmap type
        /// </summary>
        public BeatmapType Type;

        /// <summary>
        /// Date this entry was added to the playlist
        /// </summary>
        public DateTimeOffset DateAdded;

        /// <summary>
        /// BeatSaver Key
        /// </summary>
        public uint? Key;

        /// <summary>
        /// Beatmap sha1sum
        /// </summary>
        public byte[]? Hash;

        /// <summary>
        /// Beatmap zip as bytes
        /// </summary>
        public byte[]? Bytes;

        /// <summary>
        /// Beatmap level ID
        /// </summary>
        public string? LevelID;

        /// <summary>
        /// Construct a new Beatmap
        /// </summary>
        public Beatmap()
        {
            DateAdded = DateTimeOffset.Now;
        }

        /// <summary>
        /// Reads a playlist from a stream
        /// </summary>
        /// <param name="reader">Binary Reader</param>
        internal Beatmap(BlisterBinaryReader reader)
        {
            Type = (BeatmapType)reader.ReadByte();
            DateAdded = reader.ReadDateTimeOffset();
            Key = reader.ReadOptionalUInt32();
            Hash = reader.ReadOptionalBytes();
            Bytes = reader.ReadOptionalBytes();
            LevelID = reader.ReadShortString();
        }

        /// <summary>
        /// Writes the playlist to a stream
        /// </summary>
        /// <param name="writer">Binary Writer</param>
        internal void Write(BlisterBinaryWriter writer)
        {
            writer.Write((byte)Type);
            writer.WriteDateTimeOffset(DateAdded);
            writer.WriteOptionalUInt32(Key);
            writer.WriteBytes(Hash);
            writer.WriteBytes(Bytes);
            writer.WriteShortString(LevelID);
        }
    }
}
