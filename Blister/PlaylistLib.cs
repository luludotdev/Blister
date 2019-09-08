using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using Blister.Types;

namespace Blister
{
    /// <summary>
    /// Main methods for Serializing / Deserializing Playlists
    /// </summary>
    public static class PlaylistLib
    {
        private static readonly string MagicNumberString = "Blist.v2";

        /// <summary>
        /// Blister format Magic Number
        /// <br />
        /// UTF-8 Encoded "Blist.v2"
        /// </summary>
        public static readonly byte[] MagicNumber = Encoding.UTF8.GetBytes(MagicNumberString);

        /// <summary>
        /// Deserialize BSON bytes to a Playlist struct 
        /// </summary>
        /// <param name="bytes">BSON bytes</param>
        /// <returns></returns>
        public static Playlist Deserialize(byte[] bytes)
        {
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                return Deserialize(ms);
            }
        }

        private static Stream ReadMagicNumber(Stream stream)
        {
            byte[] magicBytes = new byte[MagicNumber.Length];

            for (int i = 0; i < magicBytes.Length; i++)
            {
                magicBytes[i] = (byte)stream.ReadByte();
            }

            bool hasMagicNumber = magicBytes.SequenceEqual(MagicNumber);
            if (!hasMagicNumber)
            {
                throw new InvalidMagicNumberException();
            }

            return stream;
        }

        /// <summary>
        /// Deserialize a BSON byte stream to a Playlist struct 
        /// </summary>
        /// <param name="stream">Byte Stream</param>
        /// <returns></returns>
        public static Playlist Deserialize(Stream stream)
        {
            using (Stream magic = ReadMagicNumber(stream))
            {
                using (GZipStream gzip = new GZipStream(magic, CompressionMode.Decompress))
                {
                    using (BsonDataReader reader = new BsonDataReader(gzip))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        return serializer.Deserialize<Playlist>(reader);
                    }
                }
            }
        }

        /// <summary>
        /// Serialize a playlist struct to a byte array
        /// </summary>
        /// <param name="playlist">Playlist struct</param>
        /// <returns></returns>
        public static byte[] Serialize(Playlist playlist)
        {
            using (MemoryStream ms = SerializeStream(playlist))
            {
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Serialize a playlist struct to a Memory Stream
        /// </summary>
        /// <param name="playlist">Playlist struct</param>
        /// <returns></returns>
        public static MemoryStream SerializeStream(Playlist playlist)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (GZipStream gzip = new GZipStream(ms, CompressionMode.Compress))
                {
                    using (BsonDataWriter writer = new BsonDataWriter(gzip))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Serialize(writer, playlist);
                    }

                    byte[] gzipped = ms.ToArray();
                    byte[] full = MagicNumber.Concat(gzipped).ToArray();

                    return new MemoryStream(full);
                }
            }
        }
    }
}
