using System;
using System.IO;
using System.IO.Compression;

namespace Blister.IO
{
    internal class BlisterBinaryWriter : BinaryWriter
    {
        public BlisterBinaryWriter(GZipStream output) : base(output, PlaylistLib.Encoding)
        {
            if (BitConverter.IsLittleEndian != PlaylistLib.LittleEndian)
            {
                throw new UnsupportedEndiannessException();
            }
        }

        public void WriteShortString(string? s)
        {
            if (string.IsNullOrEmpty(s))
            {
                Write((byte)0);
                return;
            }

            var bytes = PlaylistLib.Encoding.GetBytes(s);

            Write((byte)bytes.Length);
            Write(bytes);
        }

        public void WriteLongString(string? s)
        {
            if (string.IsNullOrEmpty(s))
            {
                Write((ushort)0);
                return;
            }

            var bytes = PlaylistLib.Encoding.GetBytes(s);
            Write((ushort)bytes.Length);
            Write(bytes);
        }

        public void WriteOptionalUInt32(uint? u)
        {
            if (u is null)
            {
                Write(false);
                return;
            }

            Write(true);
            Write((uint)u);
        }

        public void WriteBytes(byte[]? b)
        {
            if (b is null || b.Length == 0)
            {
                Write((uint)0);
                return;
            }

            Write((uint)b.Length);
            Write(b);
        }

        public void WriteDateTimeOffset(DateTimeOffset dto)
        {
            var timestamp = dto.ToUnixTimeSeconds();
            Write((ulong)timestamp);
        }
    }
}
