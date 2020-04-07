using System;
using System.IO;
using System.IO.Compression;

namespace Blister.IO
{
    internal class BlisterBinaryReader : BinaryReader
    {
        public BlisterBinaryReader(GZipStream input) : base(input, PlaylistLib.Encoding)
        {
            if (BitConverter.IsLittleEndian != PlaylistLib.LittleEndian)
            {
                throw new UnsupportedEndiannessException();
            }
        }

        public string ReadShortString()
        {
            var length = ReadByte();
            return length == 0 ? "" : PlaylistLib.Encoding.GetString(ReadBytes(length));
        }

        public string? ReadOptionalLongString()
        {
            var length = ReadUInt16();
            return length == 0 ? null : PlaylistLib.Encoding.GetString(ReadBytes(length));
        }

        public uint? ReadOptionalUInt32()
        {
            var isNull = ReadBoolean();

            if (isNull) return null;
            else return ReadUInt32();
        }

        public byte[] ReadBytes()
        {
            var length = (int)ReadUInt32();
            return length == 0 ? new byte[0] : ReadBytes(length);
        }

        public byte[]? ReadOptionalBytes()
        {
            var length = (int)ReadUInt32();
            return length == 0 ? null : ReadBytes(length);
        }

        public DateTimeOffset ReadDateTimeOffset()
        {
            var timestamp = (long)ReadUInt64();
            return DateTimeOffset.FromUnixTimeSeconds(timestamp);
        }
    }
}
