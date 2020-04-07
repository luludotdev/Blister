using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using Blister.IO;
using Blister.Types;

namespace Blister
{
    /// <summary>
    /// Main methods for Serializing / Deserializing Playlists
    /// </summary>
    public static class PlaylistLib
    {
        internal const string MagicNumberString = "Blist.v3";
        internal const bool LittleEndian = true;
        internal static readonly Encoding Encoding = Encoding.UTF8;

        /// <summary>
        /// Blister format Magic Number
        /// <br />
        /// UTF-8 Encoded "Blist.v3"
        /// </summary>
        public static readonly byte[] MagicNumber = Encoding.UTF8.GetBytes(MagicNumberString);

        /// <summary>
        /// Deserialize bytes to a Playlist struct
        /// </summary>
        /// <param name="bytes">Bytes</param>
        /// <returns></returns>
        public static Playlist Deserialize(byte[] bytes)
        {
            using var ms = new MemoryStream(bytes);
            return Deserialize(ms);
        }

        private static Stream ReadMagicNumber(Stream stream)
        {
            byte[] magicBytes = new byte[MagicNumber.Length];

            for (int i = 0; i < magicBytes.Length; i++)
            {
                magicBytes[i] = (byte)stream.ReadByte();
            }

            bool hasMagicNumber = magicBytes.SequenceEqual(MagicNumber);
            if (hasMagicNumber == false)
            {
                throw new InvalidMagicNumberException();
            }

            return stream;
        }

        /// <summary>
        /// Deserialize a byte stream to a Playlist struct
        /// </summary>
        /// <param name="stream">Byte Stream</param>
        /// <returns></returns>
        public static Playlist Deserialize(Stream stream)
        {
            using var magic = ReadMagicNumber(stream);
            using var gzip = new GZipStream(magic, CompressionMode.Decompress);
            using var reader = new BlisterBinaryReader(gzip);

            return new Playlist(reader);
        }

        /// <summary>
        /// Serialize a playlist struct to a byte array
        /// </summary>
        /// <param name="playlist">Playlist struct</param>
        /// <returns></returns>
        public static byte[] Serialize(Playlist playlist)
        {
            using var ms = new MemoryStream();
            SerializeStream(playlist, ms);

            return ms.ToArray();
        }

        /// <summary>
        /// Serialize a playlist struct to a Memory Stream
        /// </summary>
        /// <param name="playlist">Playlist struct</param>
        /// <param name="stream">Stream to write to</param>
        /// <returns></returns>
        public static void SerializeStream(Playlist playlist, Stream stream)
        {
            stream.Write(MagicNumber, 0, MagicNumber.Length);

            using var gzip = new GZipStream(stream, CompressionMode.Compress);
            using var writer = new BlisterBinaryWriter(gzip);

            playlist.Write(writer);
        }
    }
}
