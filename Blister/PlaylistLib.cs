using System;
using System.IO;
using System.IO.Compression;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using Blister.Types;

namespace Blister
{
    public static class PlaylistLib
    {
        public static Playlist Load(byte[] bytes)
        {
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                return Load(ms);
            }
        }

        public static Playlist Load(Stream stream)
        {
            using (GZipStream gzip = new GZipStream(stream, CompressionMode.Decompress))
            {
                using (BsonDataReader reader = new BsonDataReader(gzip))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    return serializer.Deserialize<Playlist>(reader);
                }
            }
        }

        public static byte[] Save(Playlist playlist)
        {
            using (MemoryStream ms = SaveStream(playlist))
            {
                return ms.ToArray();
            }
        }

        public static MemoryStream SaveStream(Playlist playlist)
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

                    return new MemoryStream(ms.ToArray());
                }
            }
        }
    }
}
