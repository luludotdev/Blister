using System;
using System.Linq;

namespace Blister.Conversion
{
    internal static class MimeType
    {
        private static readonly byte[] bmpBytes = { 66, 77 };
        private static readonly byte[] docBytes = { 208, 207, 17, 224, 161, 177, 26, 225 };
        private static readonly byte[] exeDllBytes = { 77, 90 };
        private static readonly byte[] gifBytes = { 71, 73, 70, 56 };
        private static readonly byte[] icoBytes = { 0, 0, 1, 0 };
        private static readonly byte[] jpgBytes = { 255, 216, 255 };
        private static readonly byte[] mp3Bytes = { 255, 251, 48 };
        private static readonly byte[] pdfBytes = { 37, 80, 68, 70, 45, 49, 46 };
        private static readonly byte[] pngBytes = { 137, 80, 78, 71, 13, 10, 26, 10, 0, 0, 0, 13, 73, 72, 68, 82 };
        private static readonly byte[] rarBytes = { 82, 97, 114, 33, 26, 7, 0 };
        private static readonly byte[] swfBytes = { 70, 87, 83 };
        private static readonly byte[] tiffBytes = { 73, 73, 42, 0 };
        private static readonly byte[] torrentBytes = { 100, 56, 58, 97, 110, 110, 111, 117, 110, 99, 101 };
        private static readonly byte[] ttfBytes = { 0, 1, 0, 0, 0 };

        public const string BMP = "image/bmp";
        public const string DOC = "application/msword";
        public const string EXE_DLL = "application/x-msdownload";
        public const string GIF = "image/gif";
        public const string ICO = "image/x-icon";
        public const string JPG = "image/jpeg";
        public const string MP3 = "audio/mpeg";
        public const string PDF = "application/pdf";
        public const string PNG = "image/png";
        public const string RAR = "application/x-rar-compressed";
        public const string SWF = "application/x-shockwave-flash";
        public const string TIFF = "image/tiff";
        public const string TORRENT = "application/x-bittorrent";
        public const string TTF = "application/x-font-ttf";
        public const string OCTET_STREAM = "application/octet-stream";

        public static string GetMimeType(byte[] file)
        {
            if (file.Take(2).SequenceEqual(bmpBytes)) return BMP;
            else if (file.Take(8).SequenceEqual(docBytes)) return DOC;
            else if (file.Take(2).SequenceEqual(exeDllBytes)) return EXE_DLL;
            else if (file.Take(4).SequenceEqual(gifBytes)) return GIF;
            else if (file.Take(4).SequenceEqual(icoBytes)) return ICO;
            else if (file.Take(3).SequenceEqual(jpgBytes)) return JPG;
            else if (file.Take(3).SequenceEqual(mp3Bytes)) return MP3;
            else if (file.Take(7).SequenceEqual(pdfBytes)) return PDF;
            else if (file.Take(16).SequenceEqual(pngBytes)) return PNG;
            else if (file.Take(7).SequenceEqual(rarBytes)) return RAR;
            else if (file.Take(3).SequenceEqual(swfBytes)) return SWF;
            else if (file.Take(4).SequenceEqual(tiffBytes)) return TIFF;
            else if (file.Take(11).SequenceEqual(torrentBytes)) return TORRENT;
            else if (file.Take(5).SequenceEqual(ttfBytes)) return TTF;
            else return OCTET_STREAM;
        }
    }
}
